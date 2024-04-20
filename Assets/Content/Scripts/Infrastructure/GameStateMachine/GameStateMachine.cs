using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace SpringJam.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> registeredStates;

        private StateFactory stateFactory;
        private IState currentState;

        public IState CurrentState => currentState;

        private GameStateMachine(StateFactory stateFactory)
        {
            registeredStates = new();

            this.stateFactory = stateFactory;
        }

        public async void Enter<T>() where T : class, IState
        {
            var newState = await ChangeState<T>();

            await newState.Enter();
        }

        public void RegisterState<T>() where T : IState
        {
            registeredStates.Add(typeof(T), stateFactory.CreateState<T>());
        }

        private async UniTask<T> ChangeState<T>() where T : class, IState
        {
            if (currentState is not null)
            {
                await currentState.Exit();

                currentState = null;
            }

            currentState = GetState<T>();

            return currentState as T;
        }

        private T GetState<T>() where T : class, IState
        {
            return registeredStates[typeof(T)] as T;
        }
    }
}
