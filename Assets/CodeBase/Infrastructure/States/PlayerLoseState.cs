using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.UI.Services.Factory;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class PlayerLoseState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;

        public PlayerLoseState(ICoroutineRunner coroutineRunner, IGameStateMachine gameStateMachine, IUIFactory uiFactory)
        {
            _coroutineRunner = coroutineRunner;
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _uiFactory.CreateLoseWindow();
            _coroutineRunner.StartCoroutine(ReloadGame(2f));//delay
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