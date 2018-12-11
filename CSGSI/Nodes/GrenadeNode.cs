using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CSGSI.Nodes
{
    /// <summary>
    /// Contains information about a grenade.
    /// </summary>
    public class GrenadeNode : NodeBase
    {
        /// <summary>
        /// The ID of this grenade. -1 if ID could not be successfully parsed.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The SteamID of the owner of this grenade.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// The current position of this grenade.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The current velocity of this grenade.
        /// </summary>
        public Vector3 Velocity { get; set; }

        /// <summary>
        /// How long (in seconds) this grenade has existed.
        /// </summary>
        public float Lifetime { get; set; }

        /// <summary>
        /// The type of this grenade.
        /// </summary>
        public GrenadeType Type { get; set; }

        /// <summary>
        /// How long (in seconds) the effect of this grenade has been active (e.g. the blooming of a smoke grenade).
        /// </summary>
        public float EffectTime { get; set; }

        /// <summary>
        /// Information about the flames that this grenade has generated (if this is a molotov/incendiary grenade).
        /// </summary>
        public List<Flame> Flames { get; set; } = new List<Flame>();

        /// <summary>
        /// Initializes a new <see cref="GrenadeNode"/> instance from the given JSON string, with the given ID.
        /// </summary>
        /// <param name="json">The JSON that this <see cref="GrenadeNode"/> should be parsed from.</param>
        /// <param name="id">The ID of this grenade.</param>
        public GrenadeNode(string json, int id)
            : base(json)
        {
            ID = id;
            Owner = GetString("owner");
            Position = GetVector3("position");
            Velocity = GetVector3("velocity");
            Lifetime = GetFloat("lifetime");
            Type = GetEnum<GrenadeType>("type");
            EffectTime = GetFloat("effecttime");

            var flamesProperty = _data.Property("flames");
            if (flamesProperty != null)
            {
                foreach (var obj in flamesProperty.Children<JObject>())
                {
                    foreach (var flame in obj.Properties())
                    {
                        Flames.Add(new Flame(flame.Name, Vector3.FromComponentString(flame.Value.ToString())));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Represents the type of a grenade.
    /// </summary>
    public enum GrenadeType
    {
        /// <summary>
        /// Unknown grenade type.
        /// </summary>
        Undefined,

        /// <summary>
        /// Flashbang.
        /// </summary>
        Flashbang,

        /// <summary>
        /// Decoy grenade.
        /// </summary>
        Decoy,

        /// <summary>
        /// Incendiary grenade (CT).
        /// </summary>
        Firebomb,

        /// <summary>
        /// Smoke grenade.
        /// </summary>
        Smoke,

        /// <summary>
        /// Molotov (T).
        /// </summary>
        Inferno,

        /// <summary>
        /// HE/Frag grenade.
        /// </summary>
        Frag
    }
}