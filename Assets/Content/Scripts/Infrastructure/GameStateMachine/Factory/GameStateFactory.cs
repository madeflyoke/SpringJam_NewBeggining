using Zenject;

namespace SpringJam.Infrastructure.StateMachine
{
    public class GameStateFactory
    {
        private readonly DiContainer container;

        public GameStateFactory(DiContainer container)
        {
            this.container = container;
        }

        public T CreateState<T>() where T : IState
        {
            return container.Resolve<T>();
        }
    }
}