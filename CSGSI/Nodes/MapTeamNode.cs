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
        /// Unknown.
        /// </summary>
        public int MatchesWonThisSeries { get; set; }

        /// <summary>
        /// Name of the team (e.g. Clan name if all players have the same tag or FACEIT teams).
        /// </summary>
        public string Name { get; set; }

        internal MapTeamNode(string json)
            : base(json)
        {
            Score = GetInt32("score");
            TimeoutsRemaining = GetInt32("timeouts_remaining");
            MatchesWonThisSeries = GetInt32("matches_won_this_series");
            Name = GetString("name");
        }
    }
}