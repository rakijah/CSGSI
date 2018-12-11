using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSGSI.Nodes
{
    /// <summary>
    /// Contains information about all previously ended rounds.
    /// </summary>
    public class MapRoundWinsNode : NodeBase, IEnumerable<RoundWinReason>
    {
        /// <summary>
        /// The list of all previously ended rounds.
        /// </summary>
        public List<RoundWinReason> RoundWinReasons { get; set; } = new List<RoundWinReason>();

        /// <summary>
        /// Initializes a new <see cref="MapRoundWinsNode"/> from the given JSON.
        /// </summary>
        /// <param name="json"></param>
        public MapRoundWinsNode(string json)
            : base(json)
        {
            int children = _data.Children().Count();
            for (int i = 1; i <= children; i++)
            {
                RoundWinReasons.Add(GetEnum<RoundWinReason>(i.ToString()));
            }
        }

        /// <summary>
        /// Get information about a specific round.
        /// </summary>
        /// <param name="round">The index of the round. 1-indexed.</param>
        /// <returns>Information about the round.</returns>
        public RoundWinReason this[int round]
        {
            get
            {
                if (round <= 0 || round > RoundWinReasons.Count)
                {
                    return RoundWinReason.Undefined;
                }

                return RoundWinReasons[round - 1];
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator{RoundWinReason}"/> to iterate through all existing rounds.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<RoundWinReason> GetEnumerator()
        {
            return RoundWinReasons.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return RoundWinReasons.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents the reason why a round was won.
    /// </summary>
    public enum RoundWinReason
    {
        /// <summary>
        /// Unknown reason.
        /// </summary>
        Undefined,

        /// <summary>
        /// Counter Terrorists won because the entire Terrorist team was eliminated.
        /// </summary>
        CtWinElimination,

        /// <summary>
        /// Terrorists won because the entire Counter Terrorist team was eliminated.
        /// </summary>
        TWinElimination,

        /// <summary>
        /// Counter Terrorists won because the bomb was defused.
        /// </summary>
        CtWinDefuse,

        /// <summary>
        /// Counter Terrorists won because the round ended.
        /// </summary>
        CtWinTime,

        /// <summary>
        /// Terrorists won because the bomb exploded.
        /// </summary>
        TWinBomb
    }
}