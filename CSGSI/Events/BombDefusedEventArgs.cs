using CSGSI.Nodes;

namespace CSGSI.Events
{
    /// <summary>
    /// A delegate to handle the BombDefused event.
    /// </summary>
    /// <param name="e"></param>
    public delegate void BombDefusedHandler(BombDefusedEventArgs e);

    /// <summary>
    /// Contains information about the BombDefused event (e.g. the defuser).
    /// </summary>
    public class BombDefusedEventArgs
    {
        /// <summary>
        /// The player that defused the bomb.
        /// </summary>
        public PlayerNode Defuser;

        /// <summary>
        /// Initializes a new <see cref="BombDefusedEventArgs"/> instance from the given information.
        /// </summary>
        /// <param name="probableDefuser">The defuser.</param>
        public BombDefusedEventArgs(PlayerNode probableDefuser)
        {
            Defuser = probableDefuser;
        }
    }
}