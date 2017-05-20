using System;

namespace CSGSI.Nodes
{
    public class PlayerStateNode : NodeBase
    {
        public readonly int Health;
        public readonly int Armor;
        public readonly bool Helmet;

        /// <summary>
        /// Goes from 0 (not flashed) to 255 (fully flashed). After getting flashed, this will slowly go back down to 0.
        /// </summary>
        public readonly int Flashed;
        public readonly int Smoked;

        /// <summary>
        /// Goes from 0 (not burning) to 255 (standing in a molotov/incendiary)
        /// </summary>
        public readonly int Burning;
        public readonly int Money;
        public readonly int RoundKills;
        public readonly int RoundKillHS;
        public readonly bool DefuseKit;

        /// <summary>
        /// The total value of the players' current weapons + equipment (armor included)
        /// </summary>
        public readonly int EquipValue;

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
            DefuseKit = GetBool("defusekit");
            EquipValue = GetInt32("equip_value");
        }
    }
}
