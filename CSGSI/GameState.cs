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
        private GameState _Previously;
        private GameState _Added;
        private AuthNode _Auth;

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
