using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CSGSI
{
    /// <summary>
    /// A sub node of a GameState object
    /// </summary>
    public class GameStateNode
    {
        private JToken m_Data;

        private GameStateNode()
        {
            m_Data = JToken.Parse("{}");
        }

        /// <summary>
        /// Initializes a new GameStateNode using a JToken object
        /// </summary>
        /// <param name="node"></param>
        public GameStateNode(JToken node)
        {
            m_Data = node;
        }

        /// <summary>
        /// Get the value of a specific subnode of this GameStateNode
        /// </summary>
        /// <param name="node">The name of the subnode to get the value of</param>
        /// <returns>The string value of the specified subnode</returns>
        public string GetValue(string node)
        {
            if (m_Data[node] == null)
                return "";
            
            return m_Data[node].ToString();
        }

        /// <summary>
        /// Get a specific subnode as a new GameStateNode
        /// </summary>
        /// <param name="node">The name of the subnode</param>
        /// <returns>A new GameStateNode object containing the subnode</returns>
        public GameStateNode GetNode(string node)
        {
            if (m_Data[node] == null)
                return GameStateNode.Empty();

            return new GameStateNode(m_Data[node]);
        }

        /// <summary>
        /// An empty GameStateNode to substitute for a null value
        /// </summary>
        /// <returns></returns>
        public static GameStateNode Empty()
        {
            return new GameStateNode();
        }

        /// <summary>
        /// Get a specific subnode as a new GameStateNode
        /// </summary>
        /// <param name="node">The name of the subnode to get the value of</param>
        /// <returns>A new GameStateNode object containing the subnode</returns>
        public GameStateNode this[string node]
        {
            get { return GetNode(node); }
        }
    }
}
