using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameOver;
using CodeBase.Services.Input;
using CodeBase.Services.Observers;
using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(IGameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitSceneKey, OnLoaded);
        }

        public void Exit()
        { }

        private void RegisterServices()
        {
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterStaticData();
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services));
            _services.RegisterSingle<IInputService>(new InputService());
            _services.RegisterSingle<IGameObserverService>(new GameObserverService());
            _services.RegisterSingle<IPlayerObserverService>(new PlayerObserverService());
            _services.RegisterSingle<IGameOverService>(new GameOverCheckService(_stateMachine, _services.Single<IPlayerObserverService>(), _services.Single<IGameObserverService>()));

            _services.RegisterSingle<ICleanupService>(new CleanupService(
                _services.Single<IGameObserverService>(),
                _services.Single<IPlayerObserverService>(),
                _services.Single<IGameOverService>(),
                _services.Single<IInputService>(),
                _services.Single<IGameFactory>()));
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameSceneKey);
        }

        private void RegisterStaticData()
        {
            var service = new StaticDataService();
            service.Load();
            _services.RegisterSingle<IStaticDataService>(service);
        }
    }
}