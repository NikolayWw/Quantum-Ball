using System;

namespace CodeBase.Services.Observers.PlayerObserver
{
    public interface IPlayerObserverService : IService
    {
        Action<float> OnPlayerDeflates { get; set; }
        Action OnPlayerHasExceededDeflationLimit { get; set; }
        Action OnPlayerHasExceededNumberShots { get; set; }
        Action OnPlayerBallDisappeared { get; set; }
        Action OnPlayerBallPresent { get; set; }
        bool IsHasExceededNumberShots { get; }
        void Cleanup();
        void SetExceededNumberShoots();
    }
}