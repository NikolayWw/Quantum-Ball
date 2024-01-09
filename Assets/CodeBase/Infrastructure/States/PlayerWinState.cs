using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.UI.Services.Factory;
using System.Collections;
using CodeBase.Services.Observers;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class PlayerWinState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameObserverService _gameObserverService;
        private readonly IUIFactory _uiFactory;

        public PlayerWinState(ICoroutineRunner coroutineRunner, IGameStateMachine gameStateMachine, IGameObserverService gameObserverService, IUIFactory uiFactory)
        {
            _coroutineRunner = coroutineRunner;
            _gameStateMachine = gameStateMachine;
            _gameObserverService = gameObserverService;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _gameObserverService.OnGameWin?.Invoke();
            _uiFactory.CreateWinWindow();
            _coroutineRunner.StartCoroutine(ReloadGame(5f));//delay
        }

        public void Exit()
        { }

        private IEnumerator ReloadGame(float delay)
        {
            yield return new WaitForSeconds(delay);
            _gameStateMachine.Enter<LoadLevelState, string>(GameConstants.GameSceneKey);
        }
    }
}