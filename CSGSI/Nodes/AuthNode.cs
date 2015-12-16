using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGSI.Nodes
{
    public class AuthNode : NodeBase
    {
        public readonly string Token;

        internal AuthNode(string JSON)
            : base(JSON)
        {
            Token = GetString("token");
        }

    }
}
