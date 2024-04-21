using System;
using DG.Tweening;
using UnityEngine;

namespace Content.Scripts.Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeScreen : MonoBehaviour
    {
        private CanvasGroup wrapper;
        private void Start()
        {
            wrapper = GetComponent<CanvasGroup>();
        }

        public void Show(float duration, Action OnComplete = null)
        {
            wrapper.DOFade(1, duration)
                .OnComplete(() =>
                {
                    OnComplete?.Invoke();
                });
        }
        
        
        public void Hide(float duration, Action OnComplete = null)
        {
            wrapper.DOFade(0, duration)
                .OnComplete(() =>
                {
                    OnComplete?.Invoke();
                });
        }
    }
}
