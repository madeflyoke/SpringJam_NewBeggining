using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Zenject;

namespace SpringJam.Infrastructure.StateMachine
{
    public class GameStateMachine : IInitializable
    {
        private readonly Dictionary<Type, IState> registeredStates;

        private GameStateFactory gameStateFactory;
        private IState currentState;

        public IState CurrentState => currentState;

        public GameStateMachine(GameStateFactory gameStateFactory)
        {
            registeredStates = new();

            this.gameStateFactory = gameStateFactory;
        }

        public void Initialize()
        {
            
            RegisterState<GamePrepareState>();
            RegisterState<ComicsState>();
            RegisterState<GameplayState>();
            RegisterState<RestartState>();

            Enter<GamePrepareState>();
        }

        public async void Enter<T>() where T : class, IState
        {
            var newState = await ChangeState<T>();

            await newState.Enter();
        }

        public void RegisterState<T>() where T : IState
        {
            registeredStates.Add(typeof(T), gameStateFactory.CreateState<T>());
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
