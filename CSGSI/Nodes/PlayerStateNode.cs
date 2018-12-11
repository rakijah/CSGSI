namespace CSGSI.Nodes
{
    /// <summary>
    /// A node containing information about a players state.
    /// </summary>
    public class PlayerStateNode : NodeBase
    {
        /// <summary>
        /// The current HP of the player.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// The current armore of the player.
        /// </summary>
        public int Armor { get; set; }

        /// <summary>
        /// Indicates whether or not the player has a helmet.
        /// </summary>
        public bool Helmet { get; set; }

        /// <summary>
        /// Goes from 0 (not flashed) to 255 (fully flashed). After getting flashed, this will slowly go back down to 0.
        /// </summary>
        public int Flashed { get; set; }

        /// <summary>
        /// Goes from 0 (not in smoke) to 255 (fully in smoke). Indicates how obscured the players vision is by the smoke, with 255 being a completely grey screen.
        /// </summary>
        public int Smoked { get; set; }

        /// <summary>
        /// Goes from 0 (not burning) to 255 (standing in a molotov/incendiary)
        /// </summary>
        public int Burning { get; set; }

        /// <summary>
        /// The current money of the player.
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// The amount of kills this player got this round.
        /// </summary>
        public int RoundKills { get; set; }

        /// <summary>
        /// The amount of headshot kills this player got this round.
        /// </summary>
        public int RoundKillHS { get; set; }

        /// <summary>
        /// The total amount of damage that this player has done this round.
        /// </summary>
        public int RoundTotalDmg { get; set; }

        /// <summary>
        /// Indicates whether or not this player has a defuse kit.
        /// </summary>
        public bool DefuseKit { get; set; }

        /// <summary>
        /// The total value of the players' current weapons + equipment (armor included)
        /// </summary>
        public int EquipValue { get; set; }

        internal PlayerStateNode(string json)
            : base(json)
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
            RoundTotalDmg = GetInt32("round_totaldmg");
            DefuseKit = GetBool("defusekit");
            EquipValue = GetInt32("equip_value");
        }
    }
}