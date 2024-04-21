using System.Collections;
using System.Collections.Generic;
using Content.Scripts.Game.Level;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpringJam.Infrastructure.StateMachine
{
    public class GamePrepareState : IState
    {
        private GameStateMachine machine;
        private LevelLauncher levelLauncher;
        public GamePrepareState(GameStateMachine machine, LevelLauncher levelLauncher)
        {
            this.machine = machine;
            this.levelLauncher = levelLauncher;
        }

        public async UniTask Enter()
        {
            levelLauncher.Init(() =>
            {
                machine.Enter<ComicsState>();
            });
        }

        public  async UniTask Exit()
        {
            //throw new System.NotImplementedException();
        }
    }

}


