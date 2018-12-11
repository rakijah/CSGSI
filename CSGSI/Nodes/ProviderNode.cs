namespace CSGSI.Nodes
{
    /// <summary>
    /// A node containing information about the entity that provided this gamestate.
    /// </summary>
    public class ProviderNode : NodeBase
    {
        /// <summary>
        /// The name of the entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The App-ID of the entity. (730 in the case of CS:GO)
        /// </summary>
        public int AppID { get; set; }

        /// <summary>
        /// The version of the game. (not delimited by '.' as it is usually shown)
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The Steam-ID of the entity.
        /// </summary>
        public string SteamID { get; private set; }

        /// <summary>
        /// A Unix time stamp of when this data was sent.
        /// </summary>
        public string TimeStamp { get; private set; }

        internal ProviderNode(string json)
            : base(json)
        {
            Name = GetString("name");
            AppID = GetInt32("appid");
            Version = GetString("version");
            SteamID = GetString("steamid");
            TimeStamp = GetString("timestamp");
        }
    }
}