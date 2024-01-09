using System;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Action OnTouchDown { get; set; }
        Action OnTouchUp { get; set; }
        void SendTouchDown();
        void SendTouchUp();
        void Cleanup();
    }
}