using Content.Scripts.Game.Level;
using Content.Scripts.Game.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace SpringJam.Infrastructure.StateMachine
{
    public class GameplayState : IState
    {
        [Inject] private LevelLauncher _levelLauncher;
        
        private GameStateMachine machine;
        private LevelLauncher levelLauncher;
        private UiContainer uiContainer;
        public GameplayState(GameStateMachine machine, LevelLauncher levelLauncher, UiContainer uiContainer)
        {
            this.machine = machine;
            this.levelLauncher = levelLauncher;
            this.uiContainer = uiContainer;
        }
        
        public async UniTask Enter()
        {
           levelLauncher.LaunchGameplay();
           uiContainer.HUD.Show();
           _levelLauncher.OnPlayerFail += RestartFromLastCheckPoint;
           levelLauncher.OnPlayerFinish += FinishGame;
        }

        private void RestartFromLastCheckPoint()
        {
            machine.Enter<RestartState>();
        }
        
        private void FinishGame()
        {
            machine.Enter<FinishGameState>();
        }

        public async UniTask Exit()
        {
            _levelLauncher.OnPlayerFail -= RestartFromLastCheckPoint;
            levelLauncher.OnPlayerFinish -= FinishGame;
        }
    }

}


