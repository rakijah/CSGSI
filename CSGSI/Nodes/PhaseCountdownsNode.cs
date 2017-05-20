using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGSI.Nodes
{
    public class PhaseCountdownsNode : NodeBase
    {
        public PhaseCountdownsPhase Phase;

        /// <summary>
        /// How long (in seconds) the current phase is going to last (unless interrupted by a phase change, i.e. Live -> Bomb).
        /// </summary>
        public int PhaseEndsIn;

        internal PhaseCountdownsNode(string JSON)
            : base(JSON)
        {
            Phase = GetEnum<PhaseCountdownsPhase>("phase");
            PhaseEndsIn = GetInt32("phase_ends_in");
        }
    }

    public enum PhaseCountdownsPhase
    {
        Undefined,
        Live,
        Over,
        FreezeTime,
        Defuse,
        Bomb,
        Timeout_T,
        Timeout_CT
    }
}