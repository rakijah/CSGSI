using CSGSI.Nodes;

namespace CSGSI.Events
{
    /// <summary>
    /// A delegate to handle the PlayerFlashed event.
    /// </summary>
    /// <param name="e"></param>
    public delegate void PlayerFlashedHandler(PlayerFlashedEventArgs e);

    /// <summary>
    /// Contains information about the PlayerFlashed event (e.g. the player that got flashed and how much).
    /// </summary>
    public class PlayerFlashedEventArgs
    {
        /// <summary>
        /// The player that was flashed.
        /// </summary>
        public PlayerNode Player;

        /// <summary>
        /// How much the player got flashed.
        /// </summary>
        public int Flashed => Player.State.Flashed;

        /// <summary>
        /// Initializes a new <see cref="PlayerFlashedEventArgs"/> instance from the given information.
        /// </summary>
        /// <param name="player">The player that got flashed.</param>
        public PlayerFlashedEventArgs(PlayerNode player)
        {
            Player = player;
        }
    }
}