using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpringJam.Game.Characters
{
    [RequireComponent(typeof(Animator), typeof(CharacterAnimationEventHandler))]
    public class CharacterAnimation : MonoBehaviour
    {
        private const string BOOL_JUMP = "Jump";
        private const string BOOL_RUNNING = "Running";

        [SerializeField] private Animator animator;
        [SerializeField] private CharacterAnimationEventHandler eventHandler;

        public CharacterAnimationEventHandler EventHandler => eventHandler;

        private void Awake()
        {
            PlayIdle();
        }

        public void PlayIdle()
        {
            SetAnimationBool(BOOL_RUNNING, false);
        }

        public void PlayRunning()
        {
            SetAnimationBool(BOOL_RUNNING, true);
        }

        public async void PlayJump()
        {
            SetAnimationBool(BOOL_JUMP, true);

            await UniTask.Yield();

            SetAnimationBool(BOOL_JUMP, false);
        }

        private void SetAnimationBool(string boolName, bool value)
        {
            animator.SetBool(boolName, value);
        }
    }
}