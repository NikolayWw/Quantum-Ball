using CodeBase.Services.Observers;
using UnityEngine;

namespace CodeBase.Obstacle
{
    public class ObstacleCollisionHandler : MonoBehaviour, IObstacle
    {
        private bool _nativelyOnPlatform;
        private IGameObserverService _observerService;
        private bool _isTouch;

        public void Construct(IGameObserverService observerService)
        {
            _observerService = observerService;
        }

        public void Touch()
        {
            if (_isTouch)
                return;

            if (_nativelyOnPlatform)
                _observerService.OnDecrementObstacleOnPlatform?.Invoke();

            _isTouch = true;
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            Platform.Platform platform = other.transform.GetComponentInParent<Platform.Platform>();
            if (platform != null)
            {
                _nativelyOnPlatform = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (_isTouch)
                return;

            if (_nativelyOnPlatform == false)
                return;

            Platform.Platform platform = other.transform.GetComponentInParent<Platform.Platform>();
            if (platform != null)
            {
                if (_nativelyOnPlatform)
                {
                    _isTouch = true;
                    _observerService.OnDecrementObstacleOnPlatform?.Invoke();
                }
            }
        }
    }
}