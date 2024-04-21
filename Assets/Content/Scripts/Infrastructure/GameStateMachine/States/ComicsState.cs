using Cysharp.Threading.Tasks;
using Main.Scripts.UI;

namespace SpringJam.Infrastructure.StateMachine
{
    public class ComicsState : IState
    {
        private PrologueComics prologue;
        private GameStateMachine machine;
        public ComicsState(PrologueComics prologue, GameStateMachine machine)
        {
            this.prologue = prologue;
            this.machine = machine;
        }
        public async UniTask Enter()
        {
            prologue.OnPrologueEnd += SwitchToGameplayState;
            prologue.Show();
            
        }

        private void SwitchToGameplayState()
        {
            machine.Enter<GameplayState>();
        }

        public async UniTask Exit()
        {
            prologue.OnPrologueEnd -= SwitchToGameplayState;
        }
    }
}