using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSGSI.Nodes
{
    /// <summary>
    /// Contains information about all grenades that currently exist.
    /// </summary>
    public class GrenadesNode : NodeBase, IEnumerable<GrenadeNode>
    {
        /// <summary>
        /// The list of all grenades that currently exist.
        /// </summary>
        public List<GrenadeNode> Grenades { get; set; } = new List<GrenadeNode>();

        /// <summary>
        /// Initializes a new <see cref="GrenadesNode"/> from the given JSON.
        /// </summary>
        /// <param name="json">The JSON that should be used to initialize this instance.</param>
        public GrenadesNode(string json)
            : base(json)
        {
            foreach (var c in _data.Properties())
            {
                int id = -1;
                int.TryParse(c.Name, out id);
                Grenades.Add(new GrenadeNode(c.Value.ToString(), id));
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{GrenadesNode}"/> with all grenades of the given type.
        /// </summary>
        /// <param name="type">The type of grenades that should be returned.</param>
        /// <returns>An <see cref="IEnumerable{GrenadeNode}"/> with all grenades of the given type.</returns>
        public IEnumerable<GrenadeNode> GetAllOfType(GrenadeType type)
        {
            return Grenades.Where(grenade => grenade.Type == type);
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator{GrenadeNode}"/> to iterate through all existing grenades.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<GrenadeNode> GetEnumerator()
        {
            return Grenades.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Grenades.GetEnumerator();
        }
    }
}