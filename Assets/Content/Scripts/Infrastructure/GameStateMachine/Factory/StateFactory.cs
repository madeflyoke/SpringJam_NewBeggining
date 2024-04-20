using Zenject;

namespace SpringJam.Infrastructure.StateMachine
{
    public class StateFactory
    {
        private readonly DiContainer container;

        public StateFactory(DiContainer container)
        {
            this.container = container;
        }

        public T CreateState<T>() where T : IState
        {
            return container.Resolve<T>();
        }
    }
}