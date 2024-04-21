using System.Collections;
using System.Collections.Generic;
using Content.Scripts.Game.Level;
using Content.Scripts.Game.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
            uiContainer.FadeScreen.Show(2, () =>
            {
                uiContainer.FadeScreen.Hide(2, () =>
                {
                    machine.Enter<GameplayState>();
                });
            });
        }

        public async UniTask Exit() { }
    }

}


