using System;

namespace CSGSI.Nodes
{
    public class MatchStatsNode : NodeBase
    {
        public readonly int Kills;
        public readonly int Assists;
        public readonly int Deaths;
        public readonly int MVPs;
        public readonly int Score;

        internal MatchStatsNode(string JSON)
            : base(JSON)
        {
            Kills = GetInt32("kills");
            Assists = GetInt32("assists");
            Deaths = GetInt32("deaths");
            MVPs = GetInt32("mvps");
            Score = GetInt32("score");
        }
    }
}
