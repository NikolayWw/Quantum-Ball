using CodeBase.Infrastructure.States;
using CodeBase.Services.Observers;
using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.StaticData.Level;

namespace CodeBase.Services.GameOver
{
    public class GameOverCheckService : IGameOverService
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameObserverService _observerService;
        private readonly IPlayerObserverService _playerObserver;

        private LevelStaticData _levelConfig;
        private int _playerBallCount;
        private int _currentObstacleOnPlatform;
        private bool _isWinOrLose;

        public GameOverCheckService(IGameStateMachine gameStateMachine, IPlayerObserverService playerObserver, IGameObserverService observerService)
        {
            _gameStateMachine = gameStateMachine;
            _playerObserver = playerObserver;
            _observerService = observerService;
        }

        public void Cleanup()
        {
            _playerObserver.OnPlayerHasExceededDeflationLimit -= EnterLoseState;
            _observerService.OnDecrementObstacleOnPlatform -= DecrementObstacleOnPlatform;
            _playerObserver.OnPlayerBallPresent -= PlayerBallPresent;
            _playerObserver.OnPlayerBallDisappeared -= PlayerBallDisappeared;
            _playerObserver.OnPlayerHasExceededNumberShots -= CheckLose;

            _playerBallCount = 0;
            _isWinOrLose = false;
            _currentObstacleOnPlatform = 0;
        }

        public void StartCheck(LevelStaticData levelConfig)
        {
            _levelConfig = levelConfig;
            _currentObstacleOnPlatform = _levelConfig.ObstacleSpawnDataContainer.SpawnData.Count;
            _playerObserver.OnPlayerHasExceededDeflationLimit += EnterLoseState;
            _observerService.OnDecrementObstacleOnPlatform += DecrementObstacleOnPlatform;
            _playerObserver.OnPlayerBallPresent += PlayerBallPresent;
            _playerObserver.OnPlayerBallDisappeared += PlayerBallDisappeared;
            _playerObserver.OnPlayerHasExceededNumberShots += CheckLose;
        }

        private void PlayerBallDisappeared()
        {
            _playerBallCount--;
            CheckWin();
            CheckLose();
        }

        private void PlayerBallPresent()
        {
            _playerBallCount++;
        }

        private void DecrementObstacleOnPlatform()
        {
            _currentObstacleOnPlatform--;

            CheckWin();
            CheckLose();
        }

        private void CheckWin()
        {
            if (_isWinOrLose)
                return;

            if (_playerBallCount > 0 || _currentObstacleOnPlatform > 0)
                return;

            _isWinOrLose = true;
            EnterWinState();
        }

        private void CheckLose()
        {
            if (_isWinOrLose)
                return;

            if (_playerBallCount > 0 || false == _playerObserver.IsHasExceededNumberShots)
                return;

            _isWinOrLose = true;
            EnterLoseState();
        }

        private void EnterLoseState()
        {
            _gameStateMachine.Enter<PlayerLoseState>();
        }

        private void EnterWinState()
        {
            _gameStateMachine.Enter<PlayerWinState>();
        }
    }
}