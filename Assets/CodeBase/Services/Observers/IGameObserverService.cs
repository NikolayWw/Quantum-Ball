using System;

namespace CodeBase.Services.Observers
{
    public interface IGameObserverService : IService
    {
        Action OnDecrementObstacleOnPlatform { get; set; }
        Action OnGameWin { get; set; }

        void Cleanup();
    }
}