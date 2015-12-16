using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGSI.Nodes
{
    public class PlayerNode : NodeBase
    {
        internal string _SteamID;
        public string SteamID { get { return _SteamID; } }
        public readonly string Name;
        public readonly string Team;
        public readonly string Clan;
        public readonly PlayerActivity Activity;
        public readonly WeaponsNode Weapons;
        public readonly MatchStatsNode MatchStats;
        public readonly PlayerStateNode State;

        internal PlayerNode(string JSON)
            : base(JSON)
        {
            _SteamID = GetString("steamid");
            Name = GetString("name");
            Team = GetString("team");
            Clan = GetString("clan");
            State = new PlayerStateNode(m_Data?.SelectToken("state")?.ToString() ?? "{}");
            Weapons = new WeaponsNode(m_Data?.SelectToken("weapons")?.ToString() ?? "{}");
            MatchStats = new MatchStatsNode(m_Data?.SelectToken("match_stats")?.ToString() ?? "{}");
            Activity = GetEnum<PlayerActivity>("activity");
        }
    }

    public enum PlayerActivity
    {
        Undefined,
        Menu,
        Playing,
        /// <summary>
        /// Console is open
        /// </summary>
        TextInput
    }
}
