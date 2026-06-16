using System;

namespace DIStudy.Mogura
{
    public interface IGameService
    {
        bool IsRunning { get; }
        event Action GameOver;
        event Action GameRestarted;
        void StartGame();
        void EndGame();
    }
}
