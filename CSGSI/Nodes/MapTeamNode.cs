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

        internal MapTeamNode(string JSON)
            : base(JSON)
        {
            Score = GetInt32("score");
        }
    }
}
