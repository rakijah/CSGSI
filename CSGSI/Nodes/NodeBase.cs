using System;
using Newtonsoft.Json.Linq;

namespace CSGSI.Nodes
{
    public class NodeBase
    {
        protected string m_JSON;
        protected JObject m_Data;

        public string JSON
        {
            get { return m_JSON; }
        }

        internal NodeBase(string JSON)
        {
            if (JSON.Equals(""))
            {
                JSON = "{}";
            }
            m_Data = JObject.Parse(JSON);
            m_JSON = JSON;
        }

        internal string GetString(string Name)
        {
            return m_Data?[Name]?.ToString() ?? "";
        }

        internal int GetInt32(string Name)
        {
            return Convert.ToInt32(m_Data[Name]?.ToString() ?? "-1");
        }

        internal T GetEnum<T>(string Name)
        {
            return (T)Enum.Parse(typeof(T), (m_Data[Name]?.ToString().Replace(" ", String.Empty) ?? "Undefined"), true);
        }

        internal bool GetBool(string Name)
        {
            return m_Data?[Name]?.ToObject<bool>() ?? false;
        }
    }
}
