using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.ComponentModel;
using CSGSI.Events;
using CSGSI.Nodes;
using System.Linq;

namespace CSGSI
{
    public delegate void NewGameStateHandler(GameState gs);

    /// <summary>
    /// A class that listens for HTTP POST requests
    /// </summary>
    public class GameStateListener : IGameStateListener
    {
        private AutoResetEvent _waitForConnection = new AutoResetEvent(false);
        private GameState _currentGameState;
        private int _port;
        private bool _running = false;
        private HttpListener _listener;

        /// <summary>
        /// The most recently received GameState
        /// </summary>
        public GameState CurrentGameState
        {
            get
            {
                return _currentGameState;
            }
            private set
            {
                if (_currentGameState == value)
                    return;
                _currentGameState = value;
                RaiseEvent(NewGameState, _currentGameState);
                if (EnableRaisingIntricateEvents)
                    ProcessGameState(_currentGameState);
            }
        }

        /// <summary>
        /// Gets the port that this GameStateListener instance is listening to
        /// </summary>
        public int Port { get { return _port; } }

        /// <summary>
        /// Gets a value indicating if the listening process is running
        /// </summary>
        public bool Running { get { return _running; } }

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
            _port = Port;
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
            _port = Convert.ToInt32(PortMatch.Groups[1].Value);
            
            _listener = new HttpListener();
            _listener.Prefixes.Add(URI);
        }

        /// <summary>
        /// Starts listening for HTTP POST requests on the specified port<para />
        /// </summary>
        /// <param name="port">The port to listen on</param>
        /// <returns>Returns true on success</returns>
        public bool Start()
        {
            if (!_running)
            {
                // Initialising only at Start of Listener (for reuse after Stop)

                _listener = new HttpListener();
                _listener.Prefixes.Add("http://localhost:" + Port + "/");
                Thread ListenerThread = new Thread(new ThreadStart(Run));
                try
                {
                    _listener.Start();
                }
                catch (HttpListenerException)
                {
                    return false;
                }
                _running = true;
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
            _running = false;
            _listener.Close();
        }

        private void Run()
        {
            while (_running)
            {
                _listener.BeginGetContext(ReceiveGameState, _listener);
                _waitForConnection.WaitOne();
                _waitForConnection.Reset();
            }
            try
            {
                _listener.Stop();
            }catch(ObjectDisposedException)
            { /* _listener was already disposed, do nothing */ }
        }

        private void ReceiveGameState(IAsyncResult result)
        {
            HttpListenerContext context;
            try
            {
                context = _listener.EndGetContext(result);
            }
            catch (ObjectDisposedException e)
            {
                // Listener was Closed due to call of Stop();
                return;
            }
            finally
            {
                _waitForConnection.Set();
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

        #region Intricate Events
        /// <summary>
        /// Determines whether intricate events such as PlayerDied are raised or ignored.
        /// </summary>
        public bool EnableRaisingIntricateEvents { get; set; } = false;

        /// <summary>
        /// Processes a GameState to determine which events have to be raised.
        /// </summary>
        /// <param name="gs"></param>
        private void ProcessGameState(GameState gs)
        {
            if (!EnableRaisingIntricateEvents)
                return;

            if (PlayerGotKill != null)
            {
                var killer = gs.Previously.AllPlayers.PlayerList.Where(player => player.MatchStats.Kills != -1).SingleOrDefault(player =>  
                            player.MatchStats.Kills < gs.AllPlayers.GetBySteamID(player.SteamID).MatchStats.Kills);
                if (killer != null)
                {
                    var probableVictim = gs.Previously.AllPlayers.PlayerList.Where(player => player.MatchStats.Deaths != -1).SingleOrDefault(player =>
                            player.MatchStats.Deaths < gs.AllPlayers.GetBySteamID(player.SteamID).MatchStats.Deaths);

                    if (probableVictim == null)
                        probableVictim = new PlayerNode("");

                    var assister = gs.Previously.AllPlayers.PlayerList.Where(player => player.MatchStats.Assists != -1).SingleOrDefault(player =>
                            player.MatchStats.Assists < gs.AllPlayers.GetBySteamID(player.SteamID).MatchStats.Assists);

                    if (assister == null)
                        assister = new PlayerNode("");

                    RaiseEvent(PlayerGotKill, new PlayerGotKillEventArgs(killer, assister, probableVictim));
                }
            }
            
            if (RoundPhaseChanged != null)
            {
                if(gs.Previously.Round.Phase != RoundPhase.Undefined && 
                   gs.Round.Phase != RoundPhase.Undefined &&
                   gs.Previously.Round.Phase != gs.Round.Phase)
                {
                    RaiseEvent(RoundPhaseChanged, new RoundPhaseChangedEventArgs(gs));
                }
            }

            if(PlayerFlashed != null)
            {
                foreach(var previousPlayer in gs.Previously.AllPlayers)
                {
                    var currentPlayer = gs.AllPlayers.GetBySteamID(previousPlayer.SteamID);
                    if(previousPlayer.State.Flashed == 0 &&
                       previousPlayer.State.Flashed < currentPlayer.State.Flashed)
                    {
                        RaiseEvent(PlayerFlashed, new PlayerFlashedEventArgs(currentPlayer));
                    }
                }
            }

            if(BombPlanted != null)
            {
                //if previous state contains any player with the bomb
                //and current state does not contain any players with the bomb
                //and the previous bombstate was undefined
                //and the current bombstate is planted
                var planter = gs.Previously.AllPlayers.PlayerList.SingleOrDefault(player => player.Weapons.WeaponList.Any(weapon => weapon.Type == WeaponType.C4));
                if (planter != null &&
                   gs.AllPlayers.PlayerList.All(player => !player.Weapons.WeaponList.Any(weapon => weapon.Type == WeaponType.C4)) &&
                   gs.Previously.Round.Bomb == BombState.Undefined && 
                   gs.Round.Bomb == BombState.Planted)
                {
                    RaiseEvent(BombPlanted, new BombPlantedEventArgs(gs.AllPlayers.GetBySteamID(planter.SteamID)));
                }
            }

            if(BombDefused != null)
            {
                if(gs.Previously.Round.Bomb == BombState.Planted &&
                   gs.Round.Bomb == BombState.Defused)
                {
                    var defuser = gs.AllPlayers.PlayerList.SingleOrDefault(player => gs.AllPlayers.GetBySteamID(player.SteamID).MatchStats.Score > player.MatchStats.Score);
                    if (defuser == null)
                        defuser = new PlayerNode("");

                    RaiseEvent(BombDefused, new BombDefusedEventArgs(defuser));
                }
            }

            if(RoundEnd != null)
            {
                if(gs.Previously.Round.Phase == RoundPhase.Live && gs.Round.Phase == RoundPhase.Over)
                {
                    RaiseEvent(RoundEnd, new RoundEndEventArgs(gs));
                }
            }

            if(RoundBegin != null)
            {
                if(gs.Previously.Round.Phase == RoundPhase.FreezeTime && gs.Round.Phase == RoundPhase.Live)
                {
                    RaiseEvent(RoundBegin, new RoundBeginEventArgs(gs));
                }
            }

            if(BombExploded != null)
            {
                if (gs.Previously.Round.Bomb == BombState.Planted && gs.Round.Bomb == BombState.Exploded)
                {
                    RaiseEvent(BombExploded);
                }
            }
        }

        /// <summary>
        /// Is raised when a player gets a kill. Includes information about the killer, the (probable) assister and the (probable) victim.<para/>
        /// This information is not included in the GameState, so the assister and victim are just derived from players whose Assists/Deaths variables changed.
        /// </summary>
        public event PlayerGotKillHandler PlayerGotKill;

        /// <summary>
        /// Is raised when the round phase changes (for example "Live", "FreezeTime" etc.).
        /// </summary>
        public event RoundPhaseChangedHandler RoundPhaseChanged;

        /// <summary>
        /// Is raised when a player is flashed. Includes information about how much the player was flashed (0 - 255).
        /// </summary>
        public event PlayerFlashedHandler PlayerFlashed;

        /// <summary>
        /// Is raised when the bomb is planted. Contains information about who planted the bomb.
        /// </summary>
        public event BombPlantedHandler BombPlanted;

        /// <summary>
        /// Is raised when the bomb is defused. Contains information about the (probable) defuser (player who's Score increased with this GameState).
        /// </summary>
        public event BombDefusedHandler BombDefused;

        /// <summary>
        /// Is raised when the bomb explodes.
        /// </summary>
        public event BombExplodedHandler BombExploded;

        /// <summary>
        /// Is raised when the round ends. Contains information about which team won the round.
        /// </summary>
        public event RoundEndHandler RoundEnd;

        /// <summary>
        /// Is raised when a round begins (i.e. exits FreezeTime). Contains information about team scores and total round amount.
        /// </summary>
        public event RoundBeginHandler RoundBegin;

        private void RaiseEvent(MulticastDelegate handler, object data)
        {
            foreach (Delegate d in handler.GetInvocationList())
            {
                if (d.Target is ISynchronizeInvoke)
                {
                    (d.Target as ISynchronizeInvoke).BeginInvoke(d, new object[] { data });
                }
                else
                {
                    d.DynamicInvoke(data);
                }
            }
        }

        private void RaiseEvent(MulticastDelegate handler)
        {
            foreach (Delegate d in handler.GetInvocationList())
            {
                if (d.Target is ISynchronizeInvoke)
                {
                    (d.Target as ISynchronizeInvoke).BeginInvoke(d, null);
                }
                else
                {
                    d.DynamicInvoke(null);
                }
            }
        }
        #endregion
    }
}
