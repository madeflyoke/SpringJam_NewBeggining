using Content.Scripts.Game.Level;
using Content.Scripts.Game.UI;
using Cysharp.Threading.Tasks;

namespace SpringJam.Infrastructure.StateMachine
{
    public class RestartState : IState
    {
        private GameStateMachine machine;
        private LevelLauncher levelLauncher;
        private UiContainer uiContainer;
        
        public RestartState(GameStateMachine machine, LevelLauncher levelLauncher, UiContainer uiContainer)
        {
            this.machine = machine;
            this.levelLauncher = levelLauncher;
            this.uiContainer = uiContainer;
        }
        
        public async UniTask Enter()
        {
            levelLauncher.Disable();
            uiContainer.FadeScreen.Show(1, () =>
            {
                machine.Enter<GameplayState>();
                uiContainer.FadeScreen.Hide(1);
            });
        }

        public async UniTask Exit() { }
    }

}


