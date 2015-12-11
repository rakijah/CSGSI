using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace CSGSI
{
    /// <summary>
    /// This object represents the entire game state 
    /// </summary>
    public class GameState
    {
        private JObject m_Data;

        private GameStateNode m_Provider;
        private GameStateNode m_Map;
        private GameStateNode m_Round;
        private GameStateNode m_Player;
        private GameStateNode m_Auth;
        private GameStateNode m_Added;
        private GameStateNode m_Previously;

        /// <summary>
        /// The "provider" subnode
        /// </summary>
        public GameStateNode Provider { get { return m_Provider; } }

        /// <summary>
        /// The "map" subnode
        /// </summary>
        public GameStateNode Map { get { return m_Map; } }

        /// <summary>
        /// The "round" subnode
        /// </summary>
        public GameStateNode Round { get { return m_Round; } }

        /// <summary>
        /// The "player" subnode
        /// </summary>
        public GameStateNode Player { get { return m_Player; } }

        /// <summary>
        /// The "auth" subnode
        /// </summary>
        public GameStateNode Auth { get { return m_Auth; } }

        /// <summary>
        /// The "added" subnode
        /// </summary>
        public GameStateNode Added { get { return m_Added; } }

        /// <summary>
        /// The "previously" subnode
        /// </summary>
        public GameStateNode Previously { get { return m_Previously; } }

        private string m_JSON;

        /// <summary>
        /// The JSON string that was used to generate this object
        /// </summary>
        public string JSON { get { return m_JSON; } }

        /// <summary>
        /// Initialises a new GameState object using a JSON string
        /// </summary>
        /// <param name="JSONstring"></param>
        public GameState(string JSONstring)
        {
            m_JSON = JSONstring;

            if (!JSONstring.Equals(""))
                m_Data = JObject.Parse(JSONstring);
            
            m_Provider =    (HasRootNode("provider")    ? new GameStateNode(m_Data["provider"]) :   GameStateNode.Empty());
            m_Map =         (HasRootNode("map")         ? new GameStateNode(m_Data["map"]) :        GameStateNode.Empty());
            m_Round =       (HasRootNode("round")       ? new GameStateNode(m_Data["round"]) :      GameStateNode.Empty());
            m_Player =      (HasRootNode("player")      ? new GameStateNode(m_Data["player"]) :     GameStateNode.Empty());
            m_Auth =        (HasRootNode("auth")        ? new GameStateNode(m_Data["auth"]) :       GameStateNode.Empty());
            m_Added =       (HasRootNode("added")       ? new GameStateNode(m_Data["added"]) :      GameStateNode.Empty());
            m_Previously =  (HasRootNode("previously")  ? new GameStateNode(m_Data["previously"]) : GameStateNode.Empty());
        }

        /// <summary>
        /// Determines if the specified node exists in this GameState object 
        /// </summary>
        /// <param name="rootnode"></param>
        /// <returns>Returns true if the specified node exists, false otherwise</returns>
        public bool HasRootNode(string rootnode)
        {
            return (m_Data != null && m_Data[rootnode] != null);
        }
    }
}
