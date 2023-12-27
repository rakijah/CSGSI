namespace CSGSI.Nodes
{
    /// <summary>
    /// A node containing information about a team.
    /// </summary>
    public class MapTeamNode : NodeBase
    {
        /// <summary>
        /// The current score of this team.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The amount of remaining time-outs this team has left.
        /// </summary>
        public int TimeoutsRemaining { get; set; }

        /// <summary>
        /// The number of matches clinched in the current series (used for BO3, BO5, etc.).
        /// </summary>
        public int MatchesWonThisSeries { get; set; }

        /// <summary>
        /// The number of rounds lost consecutively by this team (drawed rounds count as lost ones).
        /// </summary>
        public int ConsecutiveRoundLosses { get; set; }

        /// <summary>
        /// The abbreviation of the region of the team.
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// The name of the team.
        /// </summary>
        public string Name { get; set; }

        internal MapTeamNode(string json)
            : base(json)
        {
            Score = GetInt32("score");
            TimeoutsRemaining = GetInt32("timeouts_remaining");
            MatchesWonThisSeries = GetInt32("matches_won_this_series");
            ConsecutiveRoundLosses = GetInt32("consecutive_round_losses");
            Flag = GetString("flag");
            Name = GetString("name");
        }
    }
}