using System;
using Newtonsoft.Json.Linq;

namespace CSGSI.Nodes
{
    public class NodeBase
    {
        protected string _JSON;
        protected JObject _data;

        public string JSON
        {
            get { return _JSON; }
        }

        /// <summary>
        /// Whether or not this node contains data (i.e. JSON string is not empty)
        /// </summary>
        public bool HasData => !string.IsNullOrWhiteSpace(JSON);

        internal NodeBase(string JSON)
        {
            if (JSON.Equals(""))
            {
                JSON = "{}";
            }
            _data = JObject.Parse(JSON);
            _JSON = JSON;
        }

        internal string GetString(string Name)
        {
            return _data?[Name]?.ToString() ?? "";
        }

        internal int GetInt32(string Name)
        {
            return Convert.ToInt32(_data[Name]?.ToString() ?? "-1");
        }

        internal T GetEnum<T>(string Name)
        {
            return (T)Enum.Parse(typeof(T), (_data[Name]?.ToString().Replace(" ", String.Empty) ?? "Undefined"), true);
        }

        internal bool GetBool(string Name)
        {
            return _data?[Name]?.ToObject<bool>() ?? false;
        }
    }
}
