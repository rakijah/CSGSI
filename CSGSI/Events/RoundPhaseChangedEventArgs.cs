using CSGSI.Nodes;

namespace CSGSI.Events
{
    /// <summary>
    /// A delegate to handle the RoundPhaseChanged event.
    /// </summary>
    /// <param name="e"></param>
    public delegate void RoundPhaseChangedHandler(RoundPhaseChangedEventArgs e);

    /// <summary>
    /// Contains information about the PlayerGotKill event (e.g. previous and current phase).
    /// </summary>
    public class RoundPhaseChangedEventArgs
    {
        /// <summary>
        /// The phase that was active before this event was fired.
        /// </summary>
        public RoundPhase PreviousPhase;

        /// <summary>
        /// The phase that is active now.
        /// </summary>
        public RoundPhase CurrentPhase;

        /// <summary>
        /// Initializes a new <see cref="RoundPhaseChangedEventArgs"/> instance from the given <see cref="GameState"/>.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> that should be used to initialize this instance.</param>
        public RoundPhaseChangedEventArgs(GameState gameState)
        {
            PreviousPhase = gameState.Previously.Round.Phase;
            CurrentPhase = gameState.Round.Phase;
        }
    }
}