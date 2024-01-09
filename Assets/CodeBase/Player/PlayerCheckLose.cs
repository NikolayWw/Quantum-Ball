using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerCheckLose : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        private PlayerStaticData _config;
        private IPlayerObserverService _playerObserver;

        private int _currentShootCount;

        public void Construct(PlayerStaticData config, IPlayerObserverService playerObserver)
        {
            _playerObserver = playerObserver;
            _config = config;

            _currentShootCount = _config.MaxShootCount;
            _playerObserver.OnPlayerDeflates += CheckSize;
            _playerObserver.OnPlayerBallPresent += CheckShootCount;
        }

        private void OnDestroy()
        {
            _playerObserver.OnPlayerDeflates -= CheckSize;
            _playerObserver.OnPlayerBallPresent -= CheckShootCount;
        }

        private void CheckShootCount()
        {
            _currentShootCount--;
            if (_currentShootCount <= 0)
            {
                _playerObserver.SetExceededNumberShoots();
                _playerObserver.OnPlayerHasExceededNumberShots?.Invoke();
            }
        }

        private void CheckSize(float f)
        {
            if (_targetTransform.localScale.x <= _config.DeflateSizeToLose)
            {
                _playerObserver.OnPlayerDeflates -= CheckSize;
                _playerObserver.OnPlayerHasExceededDeflationLimit?.Invoke();
            }
        }
    }
}