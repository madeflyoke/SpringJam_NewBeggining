using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpringJam.Infrastructure.SceneManagment
{
    public class SceneLoadingOperation : ILoadingOperation
    {
        private readonly string sceneName;

        public SceneLoadingOperation(string sceneName)
        {
            this.sceneName = sceneName;
        }

        public async UniTask LoadAsync()
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
        }
    }
}