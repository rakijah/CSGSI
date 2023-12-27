namespace CSGSI.Nodes
{
    /// <summary>
    /// A node containing information about a weapon.
    /// </summary>
    public class WeaponNode : NodeBase
    {
        /// <summary>
        /// The name of the weapon, for example "weapon_knife".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The skin this weapon currently has, for example "cu_gut_lore".
        /// </summary>
        public string Paintkit { get; set; }

        /// <summary>
        /// The type of the weapon.
        /// </summary>
        public WeaponType Type { get; set; }

        /// <summary>
        /// The current ammunition.
        /// </summary>
        public int AmmoClip { get; set; }

        /// <summary>
        /// The maximum ammunition per magazine.
        /// </summary>
        public int AmmoClipMax { get; set; }

        /// <summary>
        /// The remaining ammunition that is not currently in the magazine.
        /// </summary>
        public int AmmoReserve { get; set; }

        /// <summary>
        /// The current state of this weapon.
        /// </summary>
        public WeaponState State { get; set; }

        internal WeaponNode(string JSON)
            : base(JSON)
        {
            Name = GetString("name");
            Paintkit = GetString("paintkit");
            Type = GetEnum<WeaponType>("type");
            AmmoClip = GetInt32("ammo_clip");
            AmmoClipMax = GetInt32("ammo_clip_max");
            AmmoReserve = GetInt32("ammo_reserve");
            State = GetEnum<WeaponState>("state");
        }
    }

    /// <summary>
    /// Represents the type of a weapon.
    /// </summary>
    public enum WeaponType
    {
        /// <summary>
        /// Unknown weapon type.
        /// </summary>
        Undefined,

        /// <summary>
        /// Rifle.
        /// </summary>
        Rifle,

        /// <summary>
        /// Sniper rifle.
        /// </summary>
        SniperRifle,

        /// <summary>
        /// Submachine gun.
        /// </summary>
        SubmachineGun,

        /// <summary>
        /// Shotgun.
        /// </summary>
        Shotgun,

        /// <summary>
        /// Machine gun.
        /// </summary>
        MachineGun,

        /// <summary>
        /// Pistol.
        /// </summary>
        Pistol,

        /// <summary>
        /// Knife.
        /// </summary>
        Knife,

        /// <summary>
        /// Grenade.
        /// </summary>
        Grenade,

        /// <summary>
        /// Bomb/C4.
        /// </summary>
        C4,

        /// <summary>
        /// Used for example for the coop stimpacks.
        /// </summary>
        StackableItem,

        /// <summary>
        /// Tablet in Danger Zone.
        /// </summary>
        Tablet,

        /// <summary>
        /// Bare fists in Danger Zone.
        /// </summary>
        Fists,

        /// <summary>
        /// Breach charge in Danger Zone.
        /// </summary>
        BreachCharge,

        /// <summary>
        /// Melee weapons (such as hammer/wrench) in Danger Zone.
        /// </summary>
        Melee
    }

    /// <summary>
    /// Represents the state of a weapon.
    /// </summary>
    public enum WeaponState
    {
        /// <summary>
        /// Unknown weapon state.
        /// </summary>
        Undefined,

        /// <summary>
        /// The weapon is currently equipped.
        /// </summary>
        Active,

        /// <summary>
        /// The weapon is not currently equipped.
        /// </summary>
        Holstered,

        /// <summary>
        /// The weapon is currently equipped and is being reloaded.
        /// </summary>
        Reloading
    }
}