using CSGSI.Events;

namespace CSGSI
{
    /// <summary>
    /// Interface for GameStateListeners.
    /// </summary>
    public interface IGameStateListener
    {
        /// <summary>
        /// The most recently received <see cref="GameState"/>.
        /// </summary>
        GameState CurrentGameState { get; }

        /// <summary>
        /// Gets or sets whether game related events should be raised.
        /// </summary>
        bool EnableRaisingIntricateEvents { get; set; }

        /// <summary>
        /// The port that is listened to.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Indicates whether this instance is currently listening.
        /// </summary>
        bool Running { get; }

        /// <summary>
        /// An event that is raised when the bomb is defused.
        /// </summary>
        event BombDefusedHandler BombDefused;

        /// <summary>
        /// An event that is raised when the bomb explodes.
        /// </summary>
        event BombExplodedHandler BombExploded;

        /// <summary>
        /// An event that is raised when the bomb is planted.
        /// </summary>
        event BombPlantedHandler BombPlanted;

        /// <summary>
        /// An event that is raised when a new <see cref="GameState"/> was received.
        /// </summary>
        event NewGameStateHandler NewGameState;

        /// <summary>
        /// An event that is raised when a player gets flashed.
        /// </summary>
        event PlayerFlashedHandler PlayerFlashed;
        
        /// <summary>
        /// An event that is raised when a round begins (i.e. exits freeze time).
        /// </summary>
        event RoundBeginHandler RoundBegin;

        /// <summary>
        /// An event that is raised when a round ends.
        /// </summary>
        event RoundEndHandler RoundEnd;

        /// <summary>
        /// An event that is raised when the round phase changes.
        /// </summary>
        event RoundPhaseChangedHandler RoundPhaseChanged;

        /// <summary>
        /// Starts listening for HTTP POST requests.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/>.</returns>
        bool Start();

        /// <summary>
        /// Stops listening for HTTP POST requests.
        /// </summary>
        void Stop();
    }
}