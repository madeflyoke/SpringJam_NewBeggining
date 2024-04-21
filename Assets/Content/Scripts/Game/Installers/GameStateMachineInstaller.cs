using SpringJam.Infrastructure.StateMachine;
using Zenject;

namespace SpringJam.Game
{

    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GamePrepareState>().FromNew().AsSingle().NonLazy();
            Container.Bind<ComicsState>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameplayState>().FromNew().AsSingle().NonLazy();
            Container.Bind<RestartState>().FromNew().AsSingle().NonLazy();
            Container.Bind<FinishGameState>().FromNew().AsSingle().NonLazy();
           

            Container.Bind<GameStateFactory>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().FromNew().AsSingle().NonLazy();
        }
    }
}