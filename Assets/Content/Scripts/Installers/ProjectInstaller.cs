using SpringJam.Infrastructure.SceneManagment;
using SpringJam.Infrastructure.StateMachine;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace SpringJam
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen loadingScreenPrefab;

        private LoadingScreen loadingScreen;

        public override void InstallBindings()
        {
            InstallLoadingScreen();
            InstallGameStateMachine();
        }

        private void InstallLoadingScreen()
        {
            loadingScreen = Container.InstantiatePrefab(loadingScreenPrefab).GetComponent<LoadingScreen>();

            Container.Bind<LoadingScreen>().FromInstance(loadingScreen);
        }

        private void InstallGameStateMachine()
        {
            var stateFactory = new StateFactory(Container);

            Container.Bind<StateFactory>().FromInstance(stateFactory);
            Container.Bind<GameStateMachine>()
                .AsSingle()
                .WithArguments(stateFactory)
                .NonLazy();

            Container.Bind<BootstrapState>()
                .AsSingle()
                .WithArguments(loadingScreen)
                .NonLazy();
            Container.Bind<GameplayState>().AsSingle().NonLazy();
        }
    }
}