namespace CSGSI.Nodes
{
    /// <summary>
    /// Contains information about the bomb.
    /// </summary>
    public class BombNode : NodeBase
    {
        /// <summary>
        /// The current state of the bomb.
        /// </summary>
        public BombState State { get; set; }

        /// <summary>
        /// The current position of the bomb.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Initializes a new <see cref="BombNode"/> from the given JSON string.
        /// </summary>
        /// <param name="json"></param>
        public BombNode(string json)
            : base(json)
        {
            State = GetEnum<BombState>("state");
            Position = GetVector3("position");
        }
    }

    /// <summary>
    /// Represents the state of the bomb.
    /// </summary>
    public enum BombState
    {
        /// <summary>
        /// Bomb is in unknown state.
        /// </summary>
        Undefined,

        /// <summary>
        /// Bomb was dropped.
        /// </summary>
        Dropped,

        /// <summary>
        /// Bomb is currently being carried by a player.
        /// </summary>
        Carried,

        /// <summary>
        /// Bomb is currently being planted.
        /// </summary>
        Planting,

        /// <summary>
        /// Bomb was planted.
        /// </summary>
        Planted,
        
        /// <summary>
        /// Bomb is currently being defused.
        /// </summary>
        Defusing,

        /// <summary>
        /// Bomb was defused.
        /// </summary>
        Defused,

        /// <summary>
        /// Bomb has exploded.
        /// </summary>
        Exploded
    }
}