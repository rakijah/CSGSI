namespace CSGSI.Nodes
{
    /// <summary>
    /// A node containing information about the current round.
    /// </summary>
    public class RoundNode : NodeBase
    {
        /// <summary>
        /// The phase that the round is currently in.
        /// </summary>
        public RoundPhase Phase { get; set; }

        /// <summary>
        /// The current state of the bomb (does not include states like "Dropped" or "Planting").
        /// </summary>
        public BombState Bomb { get; set; }

        /// <summary>
        /// The team that won the current round (only available when Phase == Over)
        /// </summary>
        public RoundWinTeam WinTeam { get; set; }

        internal RoundNode(string json)
            : base(json)
        {
            Phase = GetEnum<RoundPhase>("phase");
            Bomb = GetEnum<BombState>("bomb");
            WinTeam = GetEnum<RoundWinTeam>("win_team");
        }
    }

    /// <summary>
    /// Represents the phase of a round.
    /// </summary>
    public enum RoundPhase
    {
        /// <summary>
        /// Unknown round phase.
        /// </summary>
        Undefined,

        /// <summary>
        /// Round is live.
        /// </summary>
        Live,

        /// <summary>
        /// Round is over.
        /// </summary>
        Over,

        /// <summary>
        /// Round is currently in freeze time.
        /// </summary>
        FreezeTime
    }

    /// <summary>
    /// Represents the winning team of a round.
    /// </summary>
    public enum RoundWinTeam
    {
        /// <summary>
        /// Unknown winning team.
        /// </summary>
        Undefined,

        /// <summary>
        /// Terrorists won.
        /// </summary>
        T,

        /// <summary>
        /// Counter-Terrorists won.
        /// </summary>
        CT
    }
}