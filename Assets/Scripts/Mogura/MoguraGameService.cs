using System;

namespace DIStudy.Mogura
{
    public class MoguraGameService : IGameService
    {
        public bool IsRunning { get; private set; }

        public event Action GameOver;
        public event Action GameRestarted;

        public void StartGame()
        {
            IsRunning = true;
            GameRestarted?.Invoke();
        }

        public void EndGame()
        {
            IsRunning = false;
            GameOver?.Invoke();
        }
    }
}
