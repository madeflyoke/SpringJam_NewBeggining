using SpringJam.Infrastructure.StateMachine;
using Zenject;

namespace SpringJam.Game
{

    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ComicsState>().FromNew().AsSingle().NonLazy();

            Container.Bind<GameStateFactory>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().FromNew().AsSingle().NonLazy();
        }
    }
}