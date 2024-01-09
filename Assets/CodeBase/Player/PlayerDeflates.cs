using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.Observers;
using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerDeflates : MonoBehaviour
    {
        [SerializeField] private Transform _playerBallInitialPoint;
        [SerializeField] private Transform[] _targetsForDeflates;

        private IPlayerObserverService _playerObserver;
        private IGameObserverService _gameObserver;
        private IInputService _inputService;
        private IGameFactory _gameFactory;
        private PlayerStaticData _config;

        private (PlayerBallMove move, PlayerBallDetectedObstacle detected, PlayerBallPumpUp pumpUp) _playerBall;
        private float _startSize;
        private bool _isInputTouchDown;
        private float _deflateCurrentTime;

        public void Construct(IPlayerObserverService playerObserver, IGameObserverService gameObserver, IGameFactory gameFactory, IInputService inputService, PlayerStaticData config)
        {
            _playerObserver = playerObserver;
            _gameObserver = gameObserver;
            _inputService = inputService;
            _gameFactory = gameFactory;
            _config = config;
            _startSize = _targetsForDeflates[0].transform.localScale.x;

            _gameObserver.OnGameWin += DisableThis;
            _playerObserver.OnPlayerHasExceededDeflationLimit += DisableThis;

            _inputService.OnTouchDown += StartDeflate;
            _inputService.OnTouchUp += StopDeflate;
        }

        private void OnDestroy()
        {
            _inputService.OnTouchDown -= StartDeflate;
            _inputService.OnTouchUp -= StopDeflate;
            _gameObserver.OnGameWin -= DisableThis;
            _playerObserver.OnPlayerHasExceededDeflationLimit -= DisableThis;
        }

        private void Update()
        {
            if (_isInputTouchDown == false)
                return;

            UpdateDeflateTimer();
            UpdateDeflates();
            UpdateDistancePlayerBallPoint();
        }

        private void UpdateDistancePlayerBallPoint()
        {
            Transform target = _targetsForDeflates[0];
            float radius = target.localScale.x / 2;
            _playerBallInitialPoint.localPosition = radius * Vector3.forward;
        }

        private void StartDeflate()
        {
            if (_playerObserver.IsHasExceededNumberShots)
                return;

            _gameFactory.CreatePlayerBall(_playerBallInitialPoint.position, out _playerBall.move, out _playerBall.detected, out _playerBall.pumpUp);
            _playerObserver.OnPlayerBallPresent?.Invoke();

            _deflateCurrentTime = 0;
            _isInputTouchDown = true;
        }

        private void StopDeflate()
        {
            _isInputTouchDown = false;
            _playerBall.move.Move(transform.forward);
            _playerBall.detected.SetTime(_deflateCurrentTime);
        }

        private void UpdateDeflates()
        {
            float speed = _config.DeflateSpeed * Time.deltaTime;
            foreach (Transform target in _targetsForDeflates)
            {
                var localScale = target.localScale;
                localScale -= Vector3.one * speed;
                target.localScale = localScale;
            }

            transform.position -= Vector3.up * ((_config.DeflateSpeed / 2) * Time.deltaTime);

            float currentSize = _targetsForDeflates[0].localScale.x;

            Vector3 playerBallPosition = transform.position
                                         + -transform.up * (currentSize / 2)
                                         + transform.forward * (currentSize / 2);

            _playerBall.pumpUp.PumpUp(speed, playerBallPosition);
            float percent = currentSize / _startSize;
            _playerObserver.OnPlayerDeflates?.Invoke(percent);
        }

        private void UpdateDeflateTimer()
        {
            _deflateCurrentTime += Time.deltaTime;
        }

        private void DisableThis()
        {
            _inputService.OnTouchDown -= StartDeflate;
            _inputService.OnTouchUp -= StopDeflate;
            enabled = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_playerBallInitialPoint.position, 0.5f);
        }
    }
}