using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CSGSI.Nodes
{
    /// <summary>
    /// The base type for all nodes.
    /// </summary>
    public class NodeBase
    {
        /// <summary>
        /// The data that was passed via JSON.
        /// </summary>
        protected JObject _data;

        /// <summary>
        /// The raw JSON string that was used to construct this node.
        /// </summary>
        public string JSON { get; private set; }

        /// <summary>
        /// Whether or not this node contains data (i.e. JSON string is not empty)
        /// </summary>
        public bool HasData => !string.IsNullOrWhiteSpace(JSON);

        internal NodeBase(string json)
        {
            //for some reason empty/non existant children in the JSON are now being set to "true".
            //e.g.: "round": true
            if (json.Equals("") || json.Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                json = "{}";
            }
            _data = JObject.Parse(json);
            JSON = json;
        }

        internal string GetString(string name)
        {
            return _data?[name]?.ToString() ?? "";
        }

        internal int GetInt32(string name)
        {
            if (int.TryParse(_data[name]?.ToString() ?? "-1", out int result))
            {
                return result;
            }
            return -1;
        }

        internal T GetEnum<T>(string name)
        {
            string value = (_data[name]?.ToString().Replace(" ", String.Empty).Replace("_", string.Empty) ?? "Undefined");
            return (T)Enum.Parse(typeof(T), value, true);
        }

        internal bool GetBool(string name)
        {
            return _data?[name]?.ToObject<bool>() ?? false;
        }

        internal Vector3 GetVector3(string name)
        {
            if (_data[name] == null)
            {
                return Vector3.Empty;
            }

            return Vector3.FromComponentString(_data[name].ToString());
        }

        internal float GetFloat(string name)
        {
            if (float.TryParse(_data[name]?.ToString() ?? "-1", NumberStyles.Any, CultureInfo.InvariantCulture, out float value))
            {
                return value;
            }

            return -1;
        }
    }
}