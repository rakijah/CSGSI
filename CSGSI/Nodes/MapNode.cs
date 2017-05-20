using System;

namespace CSGSI.Nodes
{
    public class MapNode : NodeBase
    {
        /// <summary>
        /// The game mode (e.g. Deathmatch, Casual etc.)
        /// </summary>
        public readonly MapMode Mode;

        /// <summary>
        /// The name of the current map (e.g. "de_mirage")
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The current phase of the match (e.g. Live, Warmup etc.)
        /// </summary>
        public readonly MapPhase Phase;

        /// <summary>
        /// The current round number
        /// !! This is set to 0 for both warmup and pistol-round, check .Phase to prevent errors !!
        /// </summary>
        public readonly int Round;

        /// <summary>
        /// Contains information about the Counter-Terrorist team such as current score.
        /// </summary>
        public readonly MapTeamNode TeamCT;

        /// <summary>
        /// Contains information about the Terrorist team such as current score.
        /// </summary>
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
