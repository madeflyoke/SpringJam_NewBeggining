using UnityEngine;

namespace Content.Scripts.Game.UI
{
    public class UiContainer : MonoBehaviour
    {
        [SerializeField] private PrologueComics prologueComics;
        [SerializeField] private  PrologueComics finishComics;
        [SerializeField] private FadeScreen fadeScreen;
        [SerializeField] private HUD hud;
        public PrologueComics PrologueScreen => prologueComics;
        public PrologueComics FinishComics => finishComics;
        public FadeScreen FadeScreen => fadeScreen;
        public HUD HUD => hud;
    }
}
