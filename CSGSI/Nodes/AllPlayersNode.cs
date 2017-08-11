using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CSGSI.Nodes
{
    public class AllPlayersNode : NodeBase
    {
        private readonly List<PlayerNode> _players = new List<PlayerNode>();

        public IEnumerable<PlayerNode> PlayerList => _players;
        
        public PlayerNode GetByName(string Name)
        {
            PlayerNode pn = _players.Find(x => x.Name == Name);
            if (pn != null)
                return pn;
            
            return new PlayerNode("");
        }
        
        public PlayerNode GetBySteamID(string SteamID)
        {
            PlayerNode pn = _players.Find(x => x.SteamID == SteamID);
            if (pn != null)
                return pn;

            return new PlayerNode("");
        }

        public int Count { get { return _players.Count; } }

        internal AllPlayersNode(string JSON)
            : base(JSON)
        {
            foreach (JToken jt in _data.Children())
            {
                PlayerNode pn = new PlayerNode(jt.First.ToString());
                pn._steamID = jt.Value<JProperty>()?.Name ?? "";
                _players.Add(pn);
            }
        }

        /// <summary>
        /// Gets the player with index &lt;index&gt;
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PlayerNode this[int index]
        {
            get
            {
                if (index > _players.Count - 1)
                {
                    return new PlayerNode("");
                }

                return _players[index];
            }
        }

        public IEnumerator<PlayerNode> GetEnumerator()
        {
            return _players.GetEnumerator();
        }

        public List<PlayerNode> GetTeam(PlayerTeam Team)
        {
            return _players.FindAll(x => x.Team == Team);
        }
    }
}
