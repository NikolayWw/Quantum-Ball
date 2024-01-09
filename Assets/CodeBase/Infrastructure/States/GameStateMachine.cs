using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameOver;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using System;
using System.Collections.Generic;
using CodeBase.Services.Observers;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitable> _states;
        private IExitable _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadCurtain loadCurtain, ICoroutineRunner coroutineRunner, AllServices services)
        {
            _states = new Dictionary<Type, IExitable>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadCurtain,
                    services.Single<IGameFactory>(),
                    services.Single<IUIFactory>(),
                    services.Single<IStaticDataService>(),
                    services.Single<ICleanupService>(),
                    services.Single<IGameOverService>()),

                [typeof(PlayerWinState)] = new PlayerWinState(coroutineRunner, this, services.Single<IGameObserverService>(), services.Single<IUIFactory>()),
                [typeof(PlayerLoseState)] = new PlayerLoseState(coroutineRunner, this, services.Single<IUIFactory>()),
                [typeof(LoopState)] = new LoopState(),
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitable
        {
            _activeState?.Exit();
            TState state = _states[typeof(TState)] as TState;
            _activeState = state;
            return state;
        }
    }
}