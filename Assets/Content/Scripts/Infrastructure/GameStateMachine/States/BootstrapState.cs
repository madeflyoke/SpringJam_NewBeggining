using Cysharp.Threading.Tasks;
using SpringJam.Infrastructure.SceneManagment;

namespace SpringJam.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private GameStateMachine gameStateMachine;
        private LoadingScreen loadingScreen;

        public BootstrapState(GameStateMachine gameStateMachine, LoadingScreen loadingScreen)
        {
            this.gameStateMachine = gameStateMachine;
            this.loadingScreen = loadingScreen;
        }

        public async UniTask Enter()
        {
            var loadingOperations = new ILoadingOperation[]
            {
                new SceneLoadingOperation(SceneNames.GameScene),
            };

            await loadingScreen.LoadAsync(loadingOperations);

            gameStateMachine.Enter<GameplayState>();
        }

        public async UniTask Exit() { }
    }
}