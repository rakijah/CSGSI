namespace CSGSI.Nodes
{
    /// <summary>
    /// A node that contains information about the current match.
    /// </summary>
    public class MapNode : NodeBase
    {
        /// <summary>
        /// Contains information about the rounds that have already ended.
        /// </summary>
        public MapRoundWinsNode RoundWins { get; set; }

        /// <summary>
        /// The game mode (e.g. Deathmatch, Casual etc.)
        /// </summary>
        public MapMode Mode { get; set; }

        /// <summary>
        /// The name of the current map (e.g. "de_mirage")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The current phase of the match (e.g. Live, Warmup etc.)
        /// </summary>
        public MapPhase Phase { get; set; }

        /// <summary>
        /// The current round number <para/>
        /// Attention: This is set to 0 for both warmup and pistol-round, check .Phase to prevent errors.
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// Contains information about the Counter-Terrorist team such as current score.
        /// </summary>
        public MapTeamNode TeamCT { get; set; }

        /// <summary>
        /// Contains information about the Terrorist team such as current score.
        /// </summary>
        public MapTeamNode TeamT { get; set; }

        /// <summary>
        /// Presumable used for tournaments.
        /// </summary>
        public int NumMatchesToWinSeries { get; set; }

        /// <summary>
        /// The amount of people currently spectating.
        /// </summary>
        public int CurrentSpectators { get; set; }

        /// <summary>
        /// Presumably used for tournaments to keep track of how many souvenir drops have been dropped to spectators.
        /// </summary>
        public int SouvenirsTotal { get; set; }

        internal MapNode(string json)
            : base(json)
        {
            RoundWins = new MapRoundWinsNode(_data["round_wins"]?.ToString() ?? "");
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

    /// <summary>
    /// Indicates a phase of a match.
    /// </summary>
    public enum MapPhase
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// The match is currently in warmup.
        /// </summary>
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

    /// <summary>
    /// Indicates a gamemode.
    /// </summary>
    public enum MapMode
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// Casual mode
        /// </summary>
        Casual,

        /// <summary>
        /// Competitive mode
        /// </summary>
        Competitive,

        /// <summary>
        /// Deathmatch mode
        /// </summary>
        DeathMatch,

        /// <summary>
        /// Gun Game mode
        /// </summary>
        GunGameProgressive,

        /// <summary>
        /// Arms Race &amp; Demolition mode
        /// </summary>
        GunGameTRBomb,

        /// <summary>
        /// Coop-Mission mode
        /// </summary>
        CoopMission,

        /// <summary>
        /// Wingman
        /// </summary>
        ScrimComp2v2,

        /// <summary>
        /// Custom mode
        /// </summary>
        Custom,

        /// <summary>
        /// Danger Zone
        /// </summary>
        Survival
    }
}