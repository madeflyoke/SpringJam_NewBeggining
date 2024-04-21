using UnityEngine;

namespace Content.Scripts.Game.UI
{
    public class UiContainer : MonoBehaviour
    {
        [SerializeField] private PrologueComics prologueComics;
        [SerializeField] private  PrologueComics finishComics;
        [SerializeField] private FadeScreen fadeScreen;
        public PrologueComics PrologueScreen => prologueComics;
        public PrologueComics FinishComics => finishComics;
        public FadeScreen FadeScreen => fadeScreen;
    }
}
