namespace CSGSI.Events
{
    /// <summary>
    /// A delegate to handle the BombExploded event.
    /// </summary>
    /// <param name="e"></param>
    public delegate void BombExplodedHandler(BombExplodedEventArgs e);

    /// <summary>
    /// Contains information about the BombExploded event (currently none).
    /// </summary>
    public class BombExplodedEventArgs
    {
        /// <summary>
        /// Initializes a new <see cref="BombExplodedEventArgs"/> instance.
        /// </summary>
        public BombExplodedEventArgs()
        {
        }
    }
}