namespace CSGSI.Nodes
{
    /// <summary>
    /// A node containing information about a players stats.
    /// </summary>
    public class MatchStatsNode : NodeBase
    {
        /// <summary>
        /// The amount of kills this player currently has.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// The amount of assists this player currently has.
        /// </summary>
        public int Assists { get; set; }

        /// <summary>
        /// The amount of deaths this player currently has.
        /// </summary>
        public int Deaths { get; set; }

        /// <summary>
        /// The amount of MVPs this player currently has.
        /// </summary>
        public int MVPs { get; set; }

        /// <summary>
        /// The current score of the player.
        /// </summary>
        public int Score { get; set; }

        internal MatchStatsNode(string JSON)
            : base(JSON)
        {
            Kills = GetInt32("kills");
            Assists = GetInt32("assists");
            Deaths = GetInt32("deaths");
            MVPs = GetInt32("mvps");
            Score = GetInt32("score");
        }
    }
}