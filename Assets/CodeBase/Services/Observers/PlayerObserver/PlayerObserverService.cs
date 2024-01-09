using System;

namespace CodeBase.Services.Observers.PlayerObserver
{
    public class PlayerObserverService : IPlayerObserverService
    {
        public bool IsHasExceededNumberShots { get; private set; }

        public Action<float> OnPlayerDeflates { get; set; }
        public Action OnPlayerHasExceededNumberShots { get; set; }
        public Action OnPlayerHasExceededDeflationLimit { get; set; }
        public Action OnPlayerBallDisappeared { get; set; }
        public Action OnPlayerBallPresent { get; set; }

        public void Cleanup()
        {
            OnPlayerHasExceededNumberShots = null;
            IsHasExceededNumberShots = false;
            OnPlayerDeflates = null;
            OnPlayerHasExceededDeflationLimit = null;
            OnPlayerBallDisappeared = null;
            OnPlayerBallPresent = null;
        }

        public void SetExceededNumberShoots()
        {
            IsHasExceededNumberShots = true;
        }
    }
}