using UnityEngine;

namespace Content.Scripts.Game.UI
{
    public class UiContainer : MonoBehaviour
    {
        [SerializeField] private PrologueComics prologueComics;
        [SerializeField] private FadeScreen fadeScreen;
        public PrologueComics PrologueScreen => prologueComics;
        public FadeScreen FadeScreen => fadeScreen;
    }
}
