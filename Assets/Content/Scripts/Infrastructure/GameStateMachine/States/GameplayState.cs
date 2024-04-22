using Content.Scripts.Game.Level;
using Content.Scripts.Game.UI;
using Cysharp.Threading.Tasks;

namespace SpringJam.Infrastructure.StateMachine
{
    public class GameplayState : IState
    {
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
           levelLauncher.OnPlayerFail += RestartFromLastCheckPoint;
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
            levelLauncher.OnPlayerFail -= RestartFromLastCheckPoint;
            levelLauncher.OnPlayerFinish -= FinishGame;
        }
    }

}


