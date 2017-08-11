using CSGSI.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGSI.Events
{
    public delegate void PlayerGotKillHandler(PlayerGotKillEventArgs e);
    public delegate void RoundPhaseChangedHandler(RoundPhaseChangedEventArgs e);
    public delegate void PlayerFlashedHandler(PlayerFlashedEventArgs e);
    public delegate void BombPlantedHandler(BombPlantedEventArgs e);
    public delegate void BombDefusedHandler(BombDefusedEventArgs e);
    public delegate void RoundEndHandler(RoundEndEventArgs e);
    public delegate void RoundBeginHandler(RoundBeginEventArgs e);
    public delegate void BombExplodedHandler();

    public class PlayerGotKillEventArgs
    {
        /// <summary>
        /// The killer.
        /// </summary>
        public PlayerNode Killer;

        /// <summary>
        /// The assister in the kill.
        /// </summary>
        public PlayerNode Assister;

        /// <summary>
        /// The *probable* victim of the kill. The GameState does not actually contain this information, so this is just a player whos Death count went up in the same GameState.
        /// </summary>
        public PlayerNode Victim;

        /// <summary>
        /// The weapon that was used for the kill.
        /// </summary>
        public WeaponNode Weapon => Killer.Weapons.ActiveWeapon;

        public PlayerGotKillEventArgs(PlayerNode killer, PlayerNode assister, PlayerNode probableVictim)
        {
            Killer = killer;
            Assister = assister;
            Victim = probableVictim;
        }
    }

    public class RoundPhaseChangedEventArgs
    {
        /// <summary>
        /// The phase that was active before this event was fired.
        /// </summary>
        public RoundPhase PreviousPhase;

        /// <summary>
        /// The phase that is active now.
        /// </summary>
        public RoundPhase CurrentPhase;

        public RoundPhaseChangedEventArgs(GameState gameState)
        {
            PreviousPhase = gameState.Previously.Round.Phase;
            CurrentPhase = gameState.Round.Phase;
        }
    }

    public class PlayerFlashedEventArgs
    {
        /// <summary>
        /// The player that was flashed.
        /// </summary>
        public PlayerNode Player;

        /// <summary>
        /// How much the player got flashed.
        /// </summary>
        public int Flashed => Player.State.Flashed;

        public PlayerFlashedEventArgs(PlayerNode player)
        {
            Player = player;
        }
    }

    public class BombPlantedEventArgs
    {
        /// <summary>
        /// The player that (probably) planted the bomb.
        /// </summary>
        public PlayerNode Planter;

        public BombPlantedEventArgs(PlayerNode planter)
        {
            Planter = planter;
        }
    }

    public class BombDefusedEventArgs
    {
        /// <summary>
        /// The player that defused the bomb.
        /// </summary>
        public PlayerNode Defuser;

        public BombDefusedEventArgs(PlayerNode probableDefuser)
        {
            Defuser = probableDefuser;
        }
    }

    public class RoundEndEventArgs
    {
        /// <summary>
        /// The team that won the round that just ended.
        /// </summary>
        public RoundWinTeam Winner;

        public RoundEndEventArgs(GameState gameState)
        {
            Winner = gameState.Round.WinTeam;
        }
    }

    public class RoundBeginEventArgs
    {
        /// <summary>
        /// Information about the map state.
        /// </summary>
        public MapNode Map;

        /// <summary>
        /// The total amount of rounds (i.e. T Score + CT Score + 1)
        /// </summary>
        public int TotalRound => Map.TeamCT.Score + Map.TeamT.Score + 1;

        /// <summary>
        /// The current score of the terrorist team.
        /// </summary>
        public int TScore => Map.TeamT.Score;

        /// <summary>
        /// The current score of the counter-terrorist team.
        /// </summary>
        public int CTScore => Map.TeamCT.Score;

        public RoundBeginEventArgs(GameState gameState)
        {
            Map = gameState.Map;
        }
    }
}
