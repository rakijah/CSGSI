using CSGSI.Events;

namespace CSGSI
{
    public interface IGameStateListener
    {
        GameState CurrentGameState { get; }
        bool EnableRaisingIntricateEvents { get; set; }
        int Port { get; }
        bool Running { get; }

        event BombDefusedHandler BombDefused;
        event BombExplodedHandler BombExploded;
        event BombPlantedHandler BombPlanted;
        event NewGameStateHandler NewGameState;
        event PlayerFlashedHandler PlayerFlashed;
        event PlayerGotKillHandler PlayerGotKill;
        event RoundBeginHandler RoundBegin;
        event RoundEndHandler RoundEnd;
        event RoundPhaseChangedHandler RoundPhaseChanged;

        bool Start();
        void Stop();
    }
}