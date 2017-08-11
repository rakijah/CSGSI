using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

using System;

namespace CSGSI.Nodes
{
    public class WeaponsNode : NodeBase
    {
        private List<WeaponNode> _weapons = new List<WeaponNode>();

        public IEnumerable<WeaponNode> WeaponList => _weapons;

        public int Count { get { return _weapons.Count; } }

        /// <summary>
        /// The weapon/equipment the player has currently pulled out.
        /// </summary>
        public WeaponNode ActiveWeapon
        {
            get
            {
                foreach(WeaponNode w in _weapons)
                {
                    if (w.State == WeaponState.Active || w.State == WeaponState.Reloading)
                        return w;
                }

                return new WeaponNode("");
            }
        }

        internal WeaponsNode(string JSON)
            : base(JSON)
        {
            foreach(JToken jt in _data.Children())
            {
                _weapons.Add(new WeaponNode(jt.First.ToString()));
            }
        }

        /// <summary>
        /// Gets the weapon with index &lt;index&gt;
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public WeaponNode this[int index]
        {
            get
            {
                if (index > _weapons.Count - 1)
                {
                    return new WeaponNode("");
                }

                return _weapons[index];
            }
        }
    }
}
