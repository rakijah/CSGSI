using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGSI.Nodes
{
    public class MapTeamNode : NodeBase
    {
        public readonly int Score;
        public readonly int TimeoutsRemaining;

        internal MapTeamNode(string JSON)
            : base(JSON)
        {
            Score = GetInt32("score");
            TimeoutsRemaining = GetInt32("timeouts_remaining");
        }
    }
}
