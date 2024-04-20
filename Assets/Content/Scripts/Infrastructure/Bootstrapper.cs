using SpringJam.Infrastructure.SceneManagment;
using UnityEngine;

namespace SpringJam.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingScreen loadingScreen;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private async void Start()
        {
            Application.targetFrameRate = int.MaxValue;

            var loadingOperations = new ILoadingOperation[]
            {
                new SceneLoadingOperation(SceneNames.GameScene),
            };

            await loadingScreen.LoadAsync(loadingOperations);
        }
    }
}