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

        /// <summary>
        /// The position of this player in the world.
        /// </summary>
        public readonly Vector3 Position;

        /// <summary>
        /// The direction the player is currently facing.
        /// </summary>
        public readonly Vector3 Forward;

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
            Position = ParseVector(GetString("position"));
            Forward = ParseVector(GetString("forward"));
        }

        private Vector3 ParseVector(string positionAsString)
        {
            string[] posCoords = positionAsString.Split(','); //still contains whitespace at the end, but parsing doesn't fail because of this
            if (posCoords.Length == 3)
            {
                if (double.TryParse(posCoords[0], out double x) &&
                    double.TryParse(posCoords[1], out double y) &&
                    double.TryParse(posCoords[2], out double z))
                {
                    return new Vector3(x, y, z);
                }
            }
            return new Vector3(0, 0, 0);
        }
    }

    public enum PlayerActivity
    {
        Undefined,
        /// <summary>
        /// Is in a menu (also applies to opening the in game menu with ESC).
        /// </summary>
        Menu,
        /// <summary>
        /// Playing or spectating.
        /// </summary>
        Playing,
        /// <summary>
        /// Console is open.
        /// </summary>
        TextInput
    }

    public enum PlayerTeam
    {
        Undefined,
        T,
        CT
    }

    /// <summary>
    /// A 3 dimensional vector.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// The X component of the vector (left/right).
        /// </summary>
        public double X;

        /// <summary>
        /// The Y component of the vector (up/down).
        /// </summary>
        public double Y;

        /// <summary>
        /// The Z component of the vector (forward/backward).
        /// </summary>
        public double Z;
        
        /// <summary>
        /// Initialises a new vector with the given X, Y and Z components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Generates a string formatted like so: <para/>
        /// (X, Y, Z)
        /// </summary>
        /// <returns>The string representation of this vector.</returns>
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    
}