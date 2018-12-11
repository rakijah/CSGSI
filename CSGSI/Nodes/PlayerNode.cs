namespace CSGSI.Nodes
{
    /// <summary>
    /// Contains information about a player.
    /// </summary>
    public class PlayerNode : NodeBase
    {
        /// <summary>
        /// The Steam ID of this player.
        /// </summary>
        public string SteamID { get; set; }

        /// <summary>
        /// The in game name of this player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The id assigned to this player for observers
        /// Example: if ObserverSlot = 4 then a spectator can press the "4" key to switch to this players' view
        /// </summary>
        public int ObserverSlot { get; set; }

        /// <summary>
        /// The team that this player is currently on.
        /// </summary>
        public PlayerTeam Team { get; set; } //TODO: test on faceit-servers

        /// <summary>
        /// The players clan tag.
        /// </summary>
        public string Clan { get; set; }

        /// <summary>
        /// The players current activity/state.
        /// </summary>
        public PlayerActivity Activity { get; set; }

        /// <summary>
        /// Contains information about this players' current inventory
        /// </summary>
        public WeaponsNode Weapons { get; set; }

        /// <summary>
        /// Contains information such as kills/deaths
        /// </summary>
        public MatchStatsNode MatchStats { get; set; }

        /// <summary>
        /// Contains information about the players' state (i.e. health, flashbang-status etc.)
        /// </summary>
        public PlayerStateNode State { get; set; }

        /// <summary>
        /// The position of this player in the world.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The direction the player is currently facing.
        /// </summary>
        public Vector3 Forward { get; set; }

        /// <summary>
        /// The Steam ID of the player that this player is spectating.
        /// </summary>
        public string SpecTarget { get; set; }

        internal PlayerNode(string json)
            : base(json)
        {
            SteamID = GetString("steamid");
            Name = GetString("name");
            ObserverSlot = GetInt32("observer_slot");
            Team = GetEnum<PlayerTeam>("team");
            Clan = GetString("clan");
            State = new PlayerStateNode(_data?.SelectToken("state")?.ToString() ?? "{}");
            Weapons = new WeaponsNode(_data?.SelectToken("weapons")?.ToString() ?? "{}");
            MatchStats = new MatchStatsNode(_data?.SelectToken("match_stats")?.ToString() ?? "{}");
            Activity = GetEnum<PlayerActivity>("activity");
            Position = GetVector3("position");
            Forward = GetVector3("forward");
            SpecTarget = GetString("spectarget");
        }
    }

    /// <summary>
    /// Indicates a player activity/state.
    /// </summary>
    public enum PlayerActivity
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// Is in a menu (also applies to opening the in game menu with ESC).
        /// </summary>
        Menu,

        /// <summary>
        /// Playing or spectating.
        /// </summary>
        Playing,

        /// <summary>
        /// Console is open.
        /// </summary>
        TextInput
    }

    /// <summary>
    /// Represents the team of a player.
    /// </summary>
    public enum PlayerTeam
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// The terrorist team.
        /// </summary>
        T,

        /// <summary>
        /// The counter-terrorist team.
        /// </summary>
        CT
    }
}