using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CSGSI
{
    public delegate void NewGameStateHandler(GameState gs);

    /// <summary>
    /// A class that listens for HTTP POST requests
    /// </summary>
    public class GameStateListener
    {
        private AutoResetEvent waitForConnection = new AutoResetEvent(false);
        private GameState m_CurrentGameState;
        private int m_Port;
        private bool m_Running = false;
        private HttpListener m_Listener;

        /// <summary>
        /// The most recently received GameState
        /// </summary>
        public GameState CurrentGameState
        {
            get
            {
                return m_CurrentGameState;
            }
        }

        /// <summary>
        /// Gets the port that this GameStateListener instance is listening to
        /// </summary>
        public int Port { get { return m_Port; } }

        /// <summary>
        /// Gets a value indicating if the listening process is running
        /// </summary>
        public bool Running { get { return m_Running; } }

        /// <summary>
        /// Occurs after a new GameState has been received
        /// </summary>
        public event NewGameStateHandler NewGameState = delegate { };

        /// <summary>
        /// A GameStateListener that listens for connections to http://localhost:&lt;Port&gt;/
        /// </summary>
        /// <param name="Port"></param>
        public GameStateListener(int Port)
        {
            m_Port = Port;
            m_Listener = new HttpListener();
            m_Listener.Prefixes.Add("http://localhost:" + Port + "/");
        }

        /// <summary>
        /// A GameStateListener that listens for connections to the specified URI
        /// </summary>
        /// <param name="URI">The URI to listen to</param>
        public GameStateListener(string URI)
        {
            if (!URI.EndsWith("/"))
                URI += "/";

            Regex URIPattern = new Regex("^https?:\\/\\/.+:([0-9]*)\\/$", RegexOptions.IgnoreCase);
            Match PortMatch = URIPattern.Match(URI);
            if (!PortMatch.Success)
            {
                throw new ArgumentException("Not a valid URI: " + URI);
            }
            m_Port = Convert.ToInt32(PortMatch.Groups[1].Value);
            
            m_Listener = new HttpListener();
            m_Listener.Prefixes.Add(URI);
        }

        /// <summary>
        /// Starts listening for HTTP POST requests on the specified port<para />
        /// </summary>
        /// <param name="port">The port to listen on</param>
        /// <returns>Returns true on success</returns>
        public bool Start()
        {
            if (!m_Running)
            {
                Thread m_ListenerThread = new Thread(new ThreadStart(Run));
                try
                {
                    m_Listener.Start();
                }
                catch (HttpListenerException)
                {
                    return false;
                }
                m_Running = true;
                m_ListenerThread.Start();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Stops listening for HTTP POST requests
        /// </summary>
        public void Stop()
        {
            m_Running = false;
        }

        private void Run()
        {
            while (m_Running)
            {
                m_Listener.BeginGetContext(ReceiveGameState, m_Listener);
                waitForConnection.WaitOne();
                waitForConnection.Reset();
            }
            m_Listener.Stop();
        }

        private void ReceiveGameState(IAsyncResult result)
        {
            HttpListenerContext context = m_Listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            string JSON;

            waitForConnection.Set();

            using (Stream inputStream = request.InputStream)
            {
                using (StreamReader sr = new StreamReader(inputStream))
                {
                    JSON = sr.ReadToEnd();
                }
            }
            using (HttpListenerResponse response = context.Response)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.StatusDescription = "OK";
                response.Close();
            }
            m_CurrentGameState = new GameState(JSON);
            NewGameState(m_CurrentGameState);
        }
    }
}
