using System;

namespace CSGSI.Nodes
{
    public class MapNode : NodeBase
    {
        public readonly MapMode Mode;
        public readonly string Name;
        public readonly MapPhase Phase;
        public readonly int Round;
        public readonly MapTeamNode TeamCT;
        public readonly MapTeamNode TeamT;

        internal MapNode(string JSON)
            : base(JSON)
        {
            Mode = GetEnum<MapMode>("mode");
            Name = GetString("name");
            Phase = GetEnum<MapPhase>("phase");
            Round = GetInt32("round");
            TeamCT = new MapTeamNode(_Data["team_ct"]?.ToString() ?? "");
            TeamT = new MapTeamNode(_Data["team_t"]?.ToString() ?? "");
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
        CoopMission,
        Custom
    }
}
