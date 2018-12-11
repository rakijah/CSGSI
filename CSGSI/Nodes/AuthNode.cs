namespace CSGSI.Nodes
{
    /// <summary>
    /// A node that contains information related to authentication.
    /// </summary>
    public class AuthNode : NodeBase
    {
        /// <summary>
        /// The provided authentication token.
        /// </summary>
        public string Token { get; set; }

        internal AuthNode(string json)
            : base(json)
        {
            Token = GetString("token");
        }
    }
}