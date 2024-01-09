using CodeBase.Services.Factory;
using CodeBase.Services.GameOver;
using CodeBase.Services.Input;
using CodeBase.Services.Observers;
using CodeBase.Services.Observers.PlayerObserver;

namespace CodeBase.Services.Cleanup
{
    public class CleanupService : ICleanupService
    {
        private readonly IGameObserverService _observerService;
        private readonly IPlayerObserverService _playerObserverService;
        private readonly IGameOverService _overService;
        private readonly IInputService _inputService;
        private readonly IGameFactory _gameFactory;

        public CleanupService(IGameObserverService observerService, IPlayerObserverService playerObserverService,IGameOverService overService,IInputService inputService,IGameFactory gameFactory)
        {
            _observerService = observerService;
            _playerObserverService = playerObserverService;
            _overService = overService;
            _inputService = inputService;
            _gameFactory = gameFactory;
        }

        public void Cleanup()
        {
            _observerService.Cleanup();
            _inputService.Cleanup();
            _gameFactory.Cleanup();
            _overService.Cleanup();
            _playerObserverService.Cleanup();
        }
    }
}