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
        /// The current round number <para/>
        /// Attention: This is set to 0 for both warmup and pistol-round, check .Phase to prevent errors.
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

        /// <summary>
        /// Presumable used for tournaments.
        /// </summary>
        public readonly int NumMatchesToWinSeries;

        /// <summary>
        /// The amount of people currently spectating.
        /// </summary>
        public readonly int CurrentSpectators;

        /// <summary>
        /// Presumably used for tournaments to keep track of how many souvenir drops have been dropped to spectators.
        /// </summary>
        public readonly int SouvenirsTotal;

        internal MapNode(string JSON)
            : base(JSON)
        {
            Mode = GetEnum<MapMode>("mode");
            Name = GetString("name");
            Phase = GetEnum<MapPhase>("phase");
            Round = GetInt32("round");
            TeamCT = new MapTeamNode(_data["team_ct"]?.ToString() ?? "");
            TeamT = new MapTeamNode(_data["team_t"]?.ToString() ?? "");
            NumMatchesToWinSeries = GetInt32("num_matches_to_win_series");
            CurrentSpectators = GetInt32("current_spectators");
            SouvenirsTotal = GetInt32("souvenirs_total");
        }
    }

    public enum MapPhase
    {
        Undefined,
        Warmup,
        /// <summary>
        /// Match is live.
        /// </summary>
        Live,
        /// <summary>
        /// Match is currently in an intermission (e.g. half time pause).
        /// </summary>
        Intermission,
        /// <summary>
        /// Match has ended (i.e. currently displaying the end scoreboard).
        /// </summary>
        GameOver
    }

    public enum MapMode
    {
        Undefined,
        /// <summary>
        /// Casual
        /// </summary>
        Casual,
        /// <summary>
        /// Competitive
        /// </summary>
        Competitive,
        DeathMatch,
        /// <summary>
        /// Gun Game
        /// </summary>
        GunGameProgressive,
        /// <summary>
        /// Arms Race &amp; Demolition
        /// </summary>
        GunGameTRBomb,
        CoopMission,
        /// <summary>
        /// Wingman
        /// </summary>
        ScrimComp2v2,
        Custom
    }
}
