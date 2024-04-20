using SpringJam.Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace SpringJam.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;

        private void Start()
        {
            RegistrationGlobalGameStates();

            gameStateMachine.Enter<BootstrapState>();
        }

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        private void RegistrationGlobalGameStates()
        {
            gameStateMachine.RegisterState<BootstrapState>();
            gameStateMachine.RegisterState<GameplayState>();
        }
    }
}