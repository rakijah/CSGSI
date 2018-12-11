namespace CSGSI.Nodes
{
    /// <summary>
    /// A node containing information about a phases state.
    /// </summary>
    public class PhaseCountdownsNode : NodeBase
    {
        /// <summary>
        /// The phase that this node represents.
        /// </summary>
        public PhaseCountdownsPhase Phase { get; set; }

        /// <summary>
        /// How long (in seconds) the current phase is going to last (unless interrupted by a phase change, i.e. Live -> Bomb).
        /// </summary>
        public float PhaseEndsIn { get; set; }

        internal PhaseCountdownsNode(string json)
            : base(json)
        {
            Phase = GetEnum<PhaseCountdownsPhase>("phase");
            PhaseEndsIn = GetFloat("phase_ends_in");
        }
    }

    /// <summary>
    /// Indicates a phase in the match that has a cooldown associated with it.
    /// </summary>
    public enum PhaseCountdownsPhase
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// Match is live.
        /// </summary>
        Live,

        /// <summary>
        /// Round is over but the next round has not started yet.
        /// </summary>
        Over,

        /// <summary>
        /// Match is currently in freezetime.
        /// </summary>
        FreezeTime,

        /// <summary>
        /// Bomb is currently being defused.
        /// </summary>
        Defuse,

        /// <summary>
        /// Bomb was planted and is ticking down.
        /// </summary>
        Bomb,

        /// <summary>
        /// Match is currently in a time-out called by the T team.
        /// </summary>
        Timeout_T,

        /// <summary>
        /// Match is currently in a time-out called by the CT team.
        /// </summary>
        Timeout_CT
    }
}