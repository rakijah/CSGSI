using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace CSGSI.Nodes
{
    /// <summary>
    /// A node that contains a list of <see cref="PlayerNode"/>s. <para/>
    /// This is only available when watching a demo or when spectating on a server with mp_forcecamera 1
    /// </summary>
    public class AllPlayersNode : NodeBase, IEnumerable<PlayerNode>
    {
        /// <summary>
        /// Gets the amount of players in this node.
        /// </summary>
        public int Count => Players.Count;

        /// <summary>
        /// A list of all player in this node.
        /// </summary>
        public List<PlayerNode> Players { get; set; } = new List<PlayerNode>();

        /// <summary>
        /// Gets a list of all player in this node. (kept for compatibility)
        /// </summary>
        public IEnumerable<PlayerNode> PlayerList => Players;

        /// <summary>
        /// Gets a player by their in-game name.
        /// </summary>
        /// <param name="name">The name to search by.</param>
        /// <returns></returns>
        public PlayerNode GetByName(string name)
        {
            PlayerNode pn = Players.Find(x => x.Name == name);
            if (pn != null)
                return pn;

            return new PlayerNode("");
        }

        /// <summary>
        /// Gets a player by their Steam-ID.
        /// </summary>
        /// <param name="steamId">The Steam-ID to search by.</param>
        /// <returns></returns>
        public PlayerNode GetBySteamID(string steamId)
        {
            PlayerNode pn = Players.Find(x => x.SteamID == steamId);
            if (pn != null)
                return pn;

            return new PlayerNode("");
        }

        internal AllPlayersNode(string json)
            : base(json)
        {
            foreach (JToken jt in _data.Children())
            {
                PlayerNode pn = new PlayerNode(jt.First.ToString())
                {
                    SteamID = jt.Value<JProperty>()?.Name ?? ""
                };
                Players.Add(pn);
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
                if (index > Players.Count - 1)
                {
                    return new PlayerNode("");
                }

                return Players[index];
            }
        }

        /// <summary>
        /// Gets all players that are on the specified team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns></returns>
        public List<PlayerNode> GetByTeam(PlayerTeam team)
        {
            return Players.FindAll(x => x.Team == team);
        }

        /// <summary>
        /// Gets an enumerator that can be used to loop through all players in this node.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PlayerNode> GetEnumerator()
        {
            return Players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Players.GetEnumerator();
        }
    }
}