using Content.Scripts.Game.UI;
using Cysharp.Threading.Tasks;

namespace SpringJam.Infrastructure.StateMachine
{
    public class ComicsState : IState
    {
        private UiContainer uiContainer;
        private GameStateMachine machine;
        public ComicsState(UiContainer uiContainer, GameStateMachine machine)
        {
            this.uiContainer = uiContainer;
            this.machine = machine;
        }
        public async UniTask Enter()
        {
            uiContainer.PrologueScreen.OnPrologueEnd += SwitchToGameplayState;
            uiContainer.PrologueScreen.Show();
        }

        private void SwitchToGameplayState()
        {
            machine.Enter<GameplayState>();
        }

        public async UniTask Exit()
        {
            uiContainer.PrologueScreen.OnPrologueEnd += SwitchToGameplayState;
        }
    }
}