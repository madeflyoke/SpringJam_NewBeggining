using Content.Scripts.Game.Level;
using Content.Scripts.Game.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpringJam.Infrastructure.StateMachine
{
    public class FinishGameState : IState
    {
        private UiContainer uiContainer;
        private LevelLauncher levelLauncher;
        public FinishGameState(UiContainer uiContainer, LevelLauncher levelLauncher)
        {
            this.uiContainer = uiContainer;
            this.levelLauncher = levelLauncher;
        }
        public async UniTask Enter()
        {
            levelLauncher.Disable();
            uiContainer.HUD.Hide();
            uiContainer.FinishComics.OnPrologueEnd += () =>
            {
                Application.Quit();
            };
            uiContainer.FadeScreen.Show(5, () =>
            {
                uiContainer.FinishComics.gameObject.SetActive(true);
                uiContainer.FadeScreen.gameObject.SetActive(false);
                uiContainer.FinishComics.Show();
            });
          
            
        }

        public  async  UniTask Exit()
        {
        }
    }

}


