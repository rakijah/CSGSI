using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.ComponentModel;

namespace CSGSI
{
    public delegate void NewGameStateHandler(GameState gs);

    /// <summary>
    /// A class that listens for HTTP POST requests
    /// </summary>
    public class GameStateListener
    {
        private AutoResetEvent waitForConnection = new AutoResetEvent(false);
        private GameState _CurrentGameState;
        private int _Port;
        private bool _Running = false;
        private HttpListener _Listener;

        /// <summary>
        /// The most recently received GameState
        /// </summary>
        public GameState CurrentGameState
        {
            get
            {
                return _CurrentGameState;
            }
            private set
            {
                _CurrentGameState = value;
                RaiseOnNewGameState();
            }
        }

        /// <summary>
        /// Gets the port that this GameStateListener instance is listening to
        /// </summary>
        public int Port { get { return _Port; } }

        /// <summary>
        /// Gets a value indicating if the listening process is running
        /// </summary>
        public bool Running { get { return _Running; } }

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
            _Port = Port;
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
            _Port = Convert.ToInt32(PortMatch.Groups[1].Value);
            
            _Listener = new HttpListener();
            _Listener.Prefixes.Add(URI);
        }

        /// <summary>
        /// Starts listening for HTTP POST requests on the specified port<para />
        /// </summary>
        /// <param name="port">The port to listen on</param>
        /// <returns>Returns true on success</returns>
        public bool Start()
        {
            if (!_Running)
            {
                // Initialising only at Start of Listener (for reuse after Stop)

                _Listener = new HttpListener();
                _Listener.Prefixes.Add("http://localhost:" + Port + "/");
                Thread ListenerThread = new Thread(new ThreadStart(Run));
                try
                {
                    _Listener.Start();
                }
                catch (HttpListenerException)
                {
                    return false;
                }
                _Running = true;
                ListenerThread.Start();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Stops listening for HTTP POST requests
        /// </summary>
        public void Stop()
        {
            _Listener.Close();
            _Running = false;
        }

        private void Run()
        {
            while (_Running)
            {
                _Listener.BeginGetContext(ReceiveGameState, _Listener);
                waitForConnection.WaitOne();
                waitForConnection.Reset();
            }

            _Running = false;
        }

        private void ReceiveGameState(IAsyncResult result)
        {
            HttpListenerContext context;
            try
            {
                context = _Listener.EndGetContext(result);
            }
            catch (ObjectDisposedException e)
            {
                // Listener was Closed due to call of Stop();
                return;
            }
            finally
            {
                waitForConnection.Set();
            }

            HttpListenerRequest request = context.Request;
            string JSON;

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
            CurrentGameState = new GameState(JSON);
        }
        
        private void RaiseOnNewGameState()
        {
            foreach (Delegate d in NewGameState.GetInvocationList())
            {
                if (d.Target is ISynchronizeInvoke)
                {
                    (d.Target as ISynchronizeInvoke).BeginInvoke(d, new object[] { CurrentGameState });
                }
                else
                {
                    d.DynamicInvoke(CurrentGameState);
                }
            }
        }
    }
}
