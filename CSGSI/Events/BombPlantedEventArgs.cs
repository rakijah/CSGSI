using CSGSI.Nodes;

namespace CSGSI.Events
{
    /// <summary>
    /// A delegate to handle the BombPlanted event.
    /// </summary>
    /// <param name="e"></param>
    public delegate void BombPlantedHandler(BombPlantedEventArgs e);

    /// <summary>
    /// Contains information about the BombPlanted event (e.g. the planter).
    /// </summary>
    public class BombPlantedEventArgs
    {
        /// <summary>
        /// The player that planted the bomb.
        /// </summary>
        public PlayerNode Planter;

        /// <summary>
        /// Initializes a new <see cref="BombPlantedEventArgs"/> instance from the given information.
        /// </summary>
        /// <param name="planter">The planter.</param>
        public BombPlantedEventArgs(PlayerNode planter)
        {
            Planter = planter;
        }
    }
}