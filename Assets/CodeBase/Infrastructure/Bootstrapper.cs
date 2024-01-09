using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner, IAddDontDestroyOnLoad
    {
        private void Awake()
        {
            Game game = new Game(this, this);
            game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        public void AddDontDestroyOnLoad(Object target) =>
            DontDestroyOnLoad(target);
    }
}