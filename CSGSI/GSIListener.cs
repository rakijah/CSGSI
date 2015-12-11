using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Security.Principal;

namespace CSGSI
{
    /// <summary>
    /// A class that listens for HTTP POST requests and keeps track of previous game states
    /// </summary>
    public static class GSIListener
    {
        private const int MAX_GAMESTATES = 10;

        private static AutoResetEvent waitForConnection = new AutoResetEvent(false);
        private static List<GameState> GameStates = new List<GameState>();

        /// <summary>
        /// The most recently received GameState object
        /// </summary>
        public static GameState CurrentGameState
        {
            get
            {
                if (GameStates.Count > 0)
                    return GameStates[GameStates.Count - 1];
                else
                    return null;
            }
        }

        private static int m_Port;
        private static bool m_Running = false;
        private static HttpListener listener;

        /// <summary>
        /// Gets the port that is currently listening
        /// </summary>
        public static int Port { get { return m_Port; } }

        /// <summary>
        /// Gets a bool determining if the listening process is running
        /// </summary>
        public static bool Running { get { return m_Running; } }

        /// <summary>
        /// Occurs after a new GameState has been received
        /// </summary>
        public static event EventHandler NewGameState = delegate { };

        /// <summary>
        /// Starts listening for HTTP POST requests on the specified port<para />
        /// !!! Fails if the application is started without administrator privileges !!!
        /// </summary>
        /// <param name="port">The port to listen on</param>
        /// <returns>Returns true if the listener could be started, false otherwise</returns>
        public static bool Start(int port)
        {
            if (!m_Running && UacHelper.IsProcessElevated)
            {
                m_Port = port;
                listener = new HttpListener();
                listener.Prefixes.Add("http://127.0.0.1:" + port + "/");
                Thread listenerThread = new Thread(new ThreadStart(Run));
                m_Running = true;
                listenerThread.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Stops listening for HTTP POST requests
        /// </summary>
        public static void Stop()
        {
            m_Running = false;
        }

        private static void Run()
        {
            try
            {
                listener.Start();
            }
            catch (HttpListenerException)
            {
                m_Running = false;
                return;
            }
            while (m_Running)
            {
                listener.BeginGetContext(ReceiveGameState, listener);
                waitForConnection.WaitOne();
                waitForConnection.Reset();
            }
            listener.Stop();
        }

        private static void ReceiveGameState(IAsyncResult result)
        {
            HttpListenerContext context = listener.EndGetContext(result);
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

            GameState gs = new GameState(JSON);
            GameStates.Add(gs);
            NewGameState(gs, EventArgs.Empty);

            while (GameStates.Count > MAX_GAMESTATES)
                GameStates.RemoveAt(0);
        }
    }
}
