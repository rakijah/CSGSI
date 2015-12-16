using System;

namespace CSGSI.Nodes
{
    public class PlayerStateNode : NodeBase
    {
        public readonly int Health;
        public readonly int Armor;
        public readonly bool Helmet;
        public readonly int Flashed;
        public readonly int Smoked;
        public readonly int Burning;
        public readonly int Money;
        public readonly int RoundKills;
        public readonly int RoundKillHS;

        internal PlayerStateNode(string JSON)
            : base(JSON)
        {
            Health = GetInt32("health");
            Armor = GetInt32("armor");
            Helmet = GetBool("helmet");
            Flashed = GetInt32("flashed");
            Smoked = GetInt32("smoked");
            Burning = GetInt32("burning");
            Money = GetInt32("money");
            RoundKills = GetInt32("round_kills");
            RoundKillHS = GetInt32("round_killhs");
        }
    }
}
