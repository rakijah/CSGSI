using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CSGSI.Nodes;
namespace CSGSI
{
    /// <summary>
    /// This object represents the entire game state 
    /// </summary>
    public class GameState
    {
        private JObject _Data;

        private ProviderNode _Provider;
        private MapNode _Map;
        private RoundNode _Round;
        private PlayerNode _Player;
        private AllPlayersNode _AllPlayers;
        private PhaseCountdownsNode _PhaseCountdowns;
        private GameState _Previously;
        private GameState _Added;
        private AuthNode _Auth;

        /// <summary>
        /// Contains information about the game that is sending the data and the Steam user that is running the game itself.
        /// </summary>
        public ProviderNode Provider
        {
            get
            {
                if (_Provider == null)
                {
                    _Provider = new ProviderNode(_Data["provider"]?.ToString() ?? "");
                }

                return _Provider;
            }
        }
        /// <summary>
        /// Contains information about the current map and match (i.e. match score and remaining timeouts)
        /// </summary>
        public MapNode Map
        {
            get
            {
                if (_Map == null)
                {
                    _Map = new MapNode(_Data["map"]?.ToString() ?? "");
                }

                return _Map;
            }
        }
        /// <summary>
        /// Contains information about the state of the current round (e.g. phase or the winning team)
        /// </summary>
        public RoundNode Round
        {
            get
            {
                if (_Round == null)
                {
                    _Round = new RoundNode(_Data["round"]?.ToString() ?? "");
                }

                return _Round;
            }
        }
        /// <summary>
        /// Contains information about the player (i.e. in the current POV, meaning this changes frequently during spectating)
        /// </summary>
        public PlayerNode Player
        {
            get
            {
                if (_Player == null)
                {
                    _Player = new PlayerNode(_Data["player"]?.ToString() ?? "");
                }

                return _Player;
            }
        }
        /// <summary>
        /// Contains information about all players
        /// !! This node is only available when spectating the match with access to every players' POV !!
        /// </summary>
        public AllPlayersNode AllPlayers
        {
            get
            {
                if (_AllPlayers == null)
                {
                    _AllPlayers = new AllPlayersNode(_Data["allplayers"]?.ToString() ?? "");
                }

                return _AllPlayers;
            }
        }

        /// <summary>
        /// Contains information about the current "phase" that the round (e.g. bomb planted) is in and how long the phase is going to last.
        /// </summary>
        public PhaseCountdownsNode PhaseCountdowns
        {
            get
            {
                if (_PhaseCountdowns == null)
                {
                    _PhaseCountdowns = new PhaseCountdownsNode(_Data["phase_countdowns"]?.ToString() ?? "");
                }

                return _PhaseCountdowns;
            }
        }

        /// <summary>
        /// When information has changed from the previous gamestate to the current one, the old values (before the change) are stored in this node.
        /// </summary>
        public GameState Previously
        {
            get
            {
                if (_Previously == null)
                {
                    _Previously = new GameState(_Data["previously"]?.ToString() ?? "");
                }

                return _Previously;
            }
        }

        /// <summary>
        /// When information has been received, that was not present in the previous gamestate, the new values are (also) stored in this node.
        /// </summary>
        public GameState Added
        {
            get
            {
                if (_Added == null)
                {
                    _Added = new GameState(_Data["added"]?.ToString() ?? "");
                }
                return _Added;
            }
        }

        //An auth code/phrase that can be set in your gamestate_integration_*.cfg. 
        public AuthNode Auth
        {
            get
            {
                if(_Auth == null)
                {
                    _Auth = new AuthNode(_Data["auth"]?.ToString() ?? "");
                }

                return _Auth;
            }
        }


        /// <summary>
        /// The JSON string that was used to generate this object
        /// </summary>
        public readonly string JSON;
        
        /// <summary>
        /// Initialises a new GameState object using a JSON string
        /// </summary>
        /// <param name="JSONstring"></param>
        public GameState(string JSONstring)
        {
            if(JSONstring.Equals(""))
            {
                JSONstring = "{}";
            }

            JSON = JSONstring;
            _Data = JObject.Parse(JSONstring);
        }
    }
}
