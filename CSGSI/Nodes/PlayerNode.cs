using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSGSI.Nodes
{
    public class PlayerNode : NodeBase
    {

        public string SteamID { get; internal set; }
        public readonly string Name;

        /// <summary>
        /// The id assigned to this player for observers
        /// Example: if ObserverSlot = 4 then a spectator can press the "4" key to switch to this players' view
        /// </summary>
        public readonly int ObserverSlot;
        public readonly PlayerTeam Team;

        /// <summary>
        /// The players clan-tag
        /// </summary>
        public readonly string Clan;
        public readonly PlayerActivity Activity;

        /// <summary>
        /// Contains information about this players' current inventory
        /// </summary>
        public readonly WeaponsNode Weapons;

        /// <summary>
        /// Contains information such as kills/deaths
        /// </summary>
        public readonly MatchStatsNode MatchStats;

        /// <summary>
        /// Contains information about the players' state (i.e. health, flashbang-status etc.)
        /// </summary>
        public readonly PlayerStateNode State;

        public readonly Position Position;

        internal PlayerNode(string JSON)
            : base(JSON)
        {
            SteamID = GetString("steamid");
            Name = GetString("name");
            ObserverSlot = GetInt32("observer_slot");
            Team = GetEnum<PlayerTeam>("team");
            Clan = GetString("clan");
            State = new PlayerStateNode(_data?.SelectToken("state")?.ToString() ?? "{}");
            Weapons = new WeaponsNode(_data?.SelectToken("weapons")?.ToString() ?? "{}");
            MatchStats = new MatchStatsNode(_data?.SelectToken("match_stats")?.ToString() ?? "{}");
            Activity = GetEnum<PlayerActivity>("activity");
            Position = ParsePosition(GetString("position"));
            
        }

        private Position ParsePosition(string PositionAsString)
        {
            string[] posCoords = PositionAsString.Split(','); //still contains whitespace at the end, but parsing doesn't fail because of this
            if (posCoords.Length == 3)
            {
                double x = 0, y = 0, z = 0;
                if (double.TryParse(posCoords[0], out x) &&
                   double.TryParse(posCoords[1], out y) &&
                   double.TryParse(posCoords[2], out z))
                {
                    return new Position(x, y, z);
                }
            }
            return new Position(0, 0, 0);
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

    public enum PlayerTeam
    {
        Undefined,
        T,
        CT
    }

    public struct Position
    {
        double X, Y, Z;
        public Position(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }
    }
}