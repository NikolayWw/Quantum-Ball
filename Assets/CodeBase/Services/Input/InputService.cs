using System;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        public Action OnTouchDown { get; set; }
        public Action OnTouchUp { get; set; }

        public void Cleanup()
        {
            OnTouchDown = null;
            OnTouchUp = null;
        }

        public void SendTouchDown() =>
            OnTouchDown?.Invoke();

        public void SendTouchUp() =>
            OnTouchUp?.Invoke();
    }
}