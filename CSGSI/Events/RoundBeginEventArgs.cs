using CSGSI.Nodes;

namespace CSGSI.Events
{
    /// <summary>
    /// A delegate to handle the RoundBegin event.
    /// </summary>
    /// <param name="e"></param>
    public delegate void RoundBeginHandler(RoundBeginEventArgs e);

    /// <summary>
    /// Contains information about the RoundBegin event (e.g. the current map, scores of each team etc.).
    /// </summary>
    public class RoundBeginEventArgs
    {
        /// <summary>
        /// Information about the map state.
        /// </summary>
        public MapNode Map;

        /// <summary>
        /// The total amount of rounds (i.e. T Score + CT Score + 1)
        /// </summary>
        public int TotalRound => Map.TeamCT.Score + Map.TeamT.Score + 1;

        /// <summary>
        /// The current score of the terrorist team.
        /// </summary>
        public int TScore => Map.TeamT.Score;

        /// <summary>
        /// The current score of the counter-terrorist team.
        /// </summary>
        public int CTScore => Map.TeamCT.Score;

        /// <summary>
        /// Initializes a new <see cref="RoundBeginEventArgs"/> instance from the given <see cref="GameState"/>.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> that should be used to initialize this instance.</param>
        public RoundBeginEventArgs(GameState gameState)
        {
            Map = gameState.Map;
        }
    }
}