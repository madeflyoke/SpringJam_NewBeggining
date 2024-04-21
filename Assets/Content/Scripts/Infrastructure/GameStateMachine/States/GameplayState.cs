using Content.Scripts.Game.Level;
using Cysharp.Threading.Tasks;

namespace SpringJam.Infrastructure.StateMachine
{
    public class GameplayState : IState
    {
        private GameStateMachine machine;
        private LevelLauncher levelLauncher;
        public GameplayState(GameStateMachine machine, LevelLauncher levelLauncher)
        {
            this.machine = machine;
            this.levelLauncher = levelLauncher;
        }
        public async UniTask Enter()
        {
           levelLauncher.LaunchGameplay();
           levelLauncher.OnPlayerFail += RestartFromLastCheckPoint;
        }

        private void RestartFromLastCheckPoint()
        {
            machine.Enter<RestartState>();
        }

        public async UniTask Exit()
        {
            levelLauncher.OnPlayerFail -= RestartFromLastCheckPoint;
        }
    }

}


