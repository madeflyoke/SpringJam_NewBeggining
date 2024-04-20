using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SpringJam.Infrastructure.SceneManagment
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            Setup();
        }

        public async UniTask LoadAsync(params ILoadingOperation[] operations)
        {
            await EnableScreen(true);

            foreach (var operation in operations)
            {
                await operation.LoadAsync();
            }

            await EnableScreen(false);
        }

        private async UniTask EnableScreen(bool enable)
        {
            if (enable)
            {
                if (gameObject.activeSelf)
                {
                    return;
                }

                gameObject.SetActive(true);
            }

            await canvasGroup.DOFade(enable ? 1f : 0f, fadeDuration).AsyncWaitForCompletion();

            if (enable == false)
            {
                gameObject.SetActive(false);
            }
        }

        private void Setup()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            canvasGroup.alpha = 1f;

            gameObject.SetActive(true);
        }
    }
}