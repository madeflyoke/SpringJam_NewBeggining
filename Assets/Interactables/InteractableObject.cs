using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interactables
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] private InteractionZone _interactionZone;
        [SerializeField] private InteractableUIView _interactableUIView;
        [SerializeField] private bool _oneTimeInteractable;
        private CancellationTokenSource _cts;

        private void OnEnable()
        {
            _interactionZone.EnterInteraction += OnEnterInteraction;
            _interactionZone.ExitInteraction += OnExitInteraction;
        }

        private void OnDisable()
        {
            _cts?.Cancel();
            _interactionZone.EnterInteraction -= OnEnterInteraction;
            _interactionZone.ExitInteraction -= OnExitInteraction;
        }

        private void OnEnterInteraction(bool isValidTarget)
        {
            if (isValidTarget)
            {
                ProcessCatchingKey();
            }
        }

        private void OnExitInteraction()
        {
            StopCatchingKey();
        }

        private async void ProcessCatchingKey()
        {
            Debug.LogWarning("KEY");

            _cts = new CancellationTokenSource();

            while (Input.GetKeyDown(KeyCode.E)==false)
            {
                var isCanceled = await UniTask.Yield(_cts.Token).SuppressCancellationThrow();
                if (isCanceled)
                {
                    return;
                }
            }
            
            OnKeyCaught();
        }

        private void StopCatchingKey()
        {
            _cts?.Cancel();
        }

        private void OnKeyCaught()
        {
            Interact();
            _interactableUIView.HideButtonInfo();
            if (_oneTimeInteractable)
            {
                _interactionZone.Disable();
            }
        }

        protected abstract void Interact();
    }
}
