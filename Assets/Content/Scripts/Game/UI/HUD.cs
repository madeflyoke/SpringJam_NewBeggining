using System;
using DG.Tweening;
using UnityEngine;

namespace Content.Scripts.Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HUD : MonoBehaviour
    {
        [SerializeField] private GameObject ByeBTN;
        [SerializeField] private GameObject TeamUpBTN;
        private CanvasGroup canvas;

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.DOFade(0, 0);
        }

        public void Show()
        {
            canvas.DOFade(1, 2);
        }
        
        public void Hide()
        {
            canvas.DOFade(1, 2);
        }

        public void ShowByeBTN()
        {
            ByeBTN.SetActive(true);
        }
        public void HideByeBTN()
        {
            ByeBTN.SetActive(false);
        }
        

        public void ShowTeamUp()
        {
            TeamUpBTN.SetActive(true);
        }
        
        public void HideTeamUp()
        {
            TeamUpBTN.SetActive(false);
        }
    }
}
