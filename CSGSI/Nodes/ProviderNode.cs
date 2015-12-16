using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGSI.Nodes
{
    public class ProviderNode : NodeBase
    {
        public readonly string Name;
        public readonly int AppID;
        public readonly int Version;
        public readonly string SteamID;
        public readonly string TimeStamp;

        internal ProviderNode(string JSON)
            : base(JSON)
        {
            Name = GetString("name");
            AppID = GetInt32("appid");
            Version = GetInt32("version");
            SteamID = GetString("steamid");
            TimeStamp = GetString("timestamp");
        }
    }
}
