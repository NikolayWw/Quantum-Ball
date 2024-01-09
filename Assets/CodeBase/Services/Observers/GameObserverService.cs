using System;

namespace CodeBase.Services.Observers
{
    public class GameObserverService : IGameObserverService
    {
        public Action OnDecrementObstacleOnPlatform { get; set; }
        public Action OnGameWin { get; set; }

        public void Cleanup()
        {
            OnDecrementObstacleOnPlatform = null;
            OnGameWin = null;
        }
    }
}