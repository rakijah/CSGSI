using CSGSI.Nodes;
using Newtonsoft.Json.Linq;

namespace CSGSI
{
    /// <summary>
    /// This object represents the entire game state
    /// </summary>
    public class GameState
    {
        private readonly JObject _data;

        private ProviderNode _provider;
        private MapNode _map;
        private RoundNode _round;
        private GrenadesNode _grenades;
        private PlayerNode _player;
        private AllPlayersNode _allPlayers;
        private BombNode _bomb;
        private PhaseCountdownsNode _phaseCountdowns;
        private GameState _previously;
        private GameState _added;
        private AuthNode _auth;

        /// <summary>
        /// Contains information about the game that is sending the data and the Steam user that is running the game itself.
        /// </summary>
        public ProviderNode Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new ProviderNode(_data["provider"]?.ToString() ?? "");
                }

                return _provider;
            }
            set
            {
                _provider = value;
            }
        }

        /// <summary>
        /// Contains information about the current map and match (i.e. match score and remaining timeouts)
        /// </summary>
        public MapNode Map
        {
            get
            {
                if (_map == null)
                {
                    _map = new MapNode(_data["map"]?.ToString() ?? "");
                }

                return _map;
            }
            set
            {
                _map = value;
            }
        }

        /// <summary>
        /// Contains information about the state of the current round (e.g. phase or the winning team)
        /// </summary>
        public RoundNode Round
        {
            get
            {
                if (_round == null)
                {
                    string roundJson = _data["round"]?.ToString() ?? "{}";
                    _round = new RoundNode(roundJson);
                }

                return _round;
            }
            set
            {
                _round = value;
            }
        }

        /// <summary>
        /// Contains information about the grenades that currently exist.
        /// </summary>
        public GrenadesNode Grenades
        {
            get
            {
                if (_grenades == null)
                {
                    _grenades = new GrenadesNode(_data["grenades"]?.ToString() ?? "");
                }

                return _grenades;
            }
            set
            {
                _grenades = value;
            }
        }

        /// <summary>
        /// Contains information about the player (i.e. in the current POV, meaning this changes frequently during spectating)
        /// </summary>
        public PlayerNode Player
        {
            get
            {
                if (_player == null)
                {
                    _player = new PlayerNode(_data["player"]?.ToString() ?? "");
                }

                return _player;
            }
            set
            {
                _player = value;
            }
        }

        /// <summary>
        /// Contains information about all players.
        /// !! This node is only available when spectating the match with access to every players' POV !!
        /// </summary>
        public AllPlayersNode AllPlayers
        {
            get
            {
                if (_allPlayers == null)
                {
                    _allPlayers = new AllPlayersNode(_data["allplayers"]?.ToString() ?? "");
                }

                return _allPlayers;
            }
            set
            {
                _allPlayers = value;
            }
        }

        /// <summary>
        /// Contains information about the bomb.
        /// </summary>
        public BombNode Bomb
        {
            get
            {
                if (_bomb == null)
                {
                    _bomb = new BombNode(_data["bomb"]?.ToString() ?? "");
                }
                return _bomb;
            }
            set
            {
                _bomb = value;
            }
        }

        /// <summary>
        /// Contains information about the current "phase" that the round (e.g. bomb planted) is in and how long the phase is going to last.
        /// </summary>
        public PhaseCountdownsNode PhaseCountdowns
        {
            get
            {
                if (_phaseCountdowns == null)
                {
                    _phaseCountdowns = new PhaseCountdownsNode(_data["phase_countdowns"]?.ToString() ?? "");
                }

                return _phaseCountdowns;
            }
            set
            {
                _phaseCountdowns = value;
            }
        }

        /// <summary>
        /// When information has changed from the previous gamestate to the current one, the old values (before the change) are stored in this node.
        /// </summary>
        public GameState Previously
        {
            get
            {
                if (_previously == null)
                {
                    _previously = new GameState(_data["previously"]?.ToString() ?? "");
                }

                return _previously;
            }
            set
            {
                _previously = value;
            }
        }

        /// <summary>
        /// When information has been received that was not present in the previous gamestate, the new values are (also) stored in this node.
        /// </summary>
        public GameState Added
        {
            get
            {
                if (_added == null)
                {
                    _added = new GameState(_data["added"]?.ToString() ?? "");
                }
                return _added;
            }
            set
            {
                _added = value;
            }
        }

        /// <summary>
        /// An auth code/phrase that can be set in your gamestate_integration_*.cfg.
        /// </summary>
        public AuthNode Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthNode(_data["auth"]?.ToString() ?? "");
                }

                return _auth;
            }
            set
            {
                _auth = value;
            }
        }

        /// <summary>
        /// The JSON string that was used to generate this object
        /// </summary>
        public string JSON { get; private set; }

        /// <summary>
        /// Initialises a new GameState object using a JSON string
        /// </summary>
        /// <param name="JSONstring"></param>
        public GameState(string JSONstring)
        {
            if (JSONstring.Equals(""))
            {
                JSONstring = "{}";
            }

            JSON = JSONstring;
            _data = JObject.Parse(JSONstring);
        }
    }
}