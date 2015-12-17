using System;
using Newtonsoft.Json.Linq;

namespace CSGSI.Nodes
{
    public class NodeBase
    {
        protected string _JSON;
        protected JObject _Data;

        public string JSON
        {
            get { return _JSON; }
        }

        internal NodeBase(string JSON)
        {
            if (JSON.Equals(""))
            {
                JSON = "{}";
            }
            _Data = JObject.Parse(JSON);
            _JSON = JSON;
        }

        internal string GetString(string Name)
        {
            return _Data?[Name]?.ToString() ?? "";
        }

        internal int GetInt32(string Name)
        {
            return Convert.ToInt32(_Data[Name]?.ToString() ?? "-1");
        }

        internal T GetEnum<T>(string Name)
        {
            return (T)Enum.Parse(typeof(T), (_Data[Name]?.ToString().Replace(" ", String.Empty) ?? "Undefined"), true);
        }

        internal bool GetBool(string Name)
        {
            return _Data?[Name]?.ToObject<bool>() ?? false;
        }
    }
}
