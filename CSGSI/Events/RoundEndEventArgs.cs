using CSGSI.Nodes;
using System.Linq;

namespace CSGSI.Events
{
    /// <summary>
    /// A delegate to handle the RoundEnd event.
    /// </summary>
    /// <param name="e"></param>
    public delegate void RoundEndHandler(RoundEndEventArgs e);

    /// <summary>
    /// Contains information about the RoundEnd event (e.g. the team that won this round).
    /// </summary>
    public class RoundEndEventArgs
    {
        /// <summary>
        /// The team that won the round that just ended.
        /// </summary>
        public RoundWinTeam Winner;

        /// <summary>
        /// The reason why this round ended.
        /// </summary>
        public RoundWinReason Reason;

        /// <summary>
        /// Initializes a new <see cref="RoundEndEventArgs"/> instance from the given <see cref="GameState"/>.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> that should be used to initialize this instance.</param>
        public RoundEndEventArgs(GameState gameState)
        {
            Winner = gameState.Round.WinTeam;

            //relying on the fact that the rounds are listed in the correct order...
            Reason = gameState.Map.RoundWins.RoundWinReasons.LastOrDefault();
        }
    }
}