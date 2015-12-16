using System;

namespace CSGSI.Nodes
{
    public class MapNode : NodeBase
    {
        public readonly MapMode Mode;
        public readonly string Name;
        public readonly MapPhase Phase;
        public readonly int Round;
        public readonly int TeamCT;
        public readonly int TeamT;

        internal MapNode(string JSON)
            : base(JSON)
        {
            Mode = GetEnum<MapMode>("mode");
            Phase = GetEnum<MapPhase>("phase");
            Round = GetInt32("round");
        }
    }

    public enum MapPhase
    {
        Undefined,
        Warmup,
        Live,
        Intermission,
        GameOver
    }

    public enum MapMode
    {
        Undefined,
        Casual,
        Competitive,
        DeathMatch,
        /// <summary>
        /// Gun Game
        /// </summary>
        GunGameProgressive,
        /// <summary>
        /// Arms Race & Demolition
        /// </summary>
        GunGameTRBomb,
        Custom
    }
}
