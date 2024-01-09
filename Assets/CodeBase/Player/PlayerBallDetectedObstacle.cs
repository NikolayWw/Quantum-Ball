using CodeBase.Obstacle;
using CodeBase.Services.Observers;
using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerBallDetectedObstacle : MonoBehaviour
    {
        private readonly Collider[] _colliders = new Collider[15];

        private PlayerStaticData _config;
        private float _inflationTime;
        private IPlayerObserverService _playerObserver;
        private IGameObserverService _gameObserverService;

        public void Construct(IPlayerObserverService playerObserver, IGameObserverService gameObserverService, PlayerStaticData config)
        {
            _playerObserver = playerObserver;
            _gameObserverService = gameObserverService;
            _config = config;
            _gameObserverService.OnGameWin += CloseThis;
        }

        private void OnDestroy()
        {
            _gameObserverService.OnGameWin -= CloseThis;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.attachedRigidbody != null
                && other.collider.attachedRigidbody.TryGetComponent(out IObstacle _))
            {
                int collidersCount = Physics.OverlapSphereNonAlloc(transform.position, CalculateRadius(), _colliders);
                for (int i = 0; i < collidersCount; i++)
                    if (_colliders[i].attachedRigidbody != null
                        && _colliders[i].attachedRigidbody.TryGetComponent(out IObstacle obstacle))
                    {
                        obstacle.Touch();
                    }
            }
            _playerObserver.OnPlayerBallDisappeared?.Invoke();
            Destroy(gameObject);
        }

        public void SetTime(float inflationTime)
        {
            _inflationTime = inflationTime;
        }

        private float CalculateRadius()
        {
            float radius = Mathf.Lerp(0, _config.MaxInfectionRadius, _inflationTime / _config.SecondsForMaxInfectionRadius);
            return radius;
        }

        private void CloseThis()
        {
            Destroy(gameObject);
        }
    }
}