using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interactables.Interfaces;
using UnityEngine;

namespace Interactables
{
    public abstract class BaseInteractableObject : MonoBehaviour
    {
        [SerializeField] protected InteractionZone InteractionZone;
        [SerializeField] private InteractableUIView _interactableUIView;
        private CancellationTokenSource _cts;

        protected virtual void OnEnable()
        {
            InteractionZone.EnterInteractionZone += OnEnterInteractionZone;
            InteractionZone.ExitInteractionZone += OnExitInteractionZone;
        }

        protected virtual void OnDisable()
        {
            _cts?.Cancel();
            InteractionZone.EnterInteractionZone -= OnEnterInteractionZone;
            InteractionZone.ExitInteractionZone -= OnExitInteractionZone;
        }

        private void OnEnterInteractionZone(IInteractor interactor)
        {
            _interactableUIView.ShowButtonInfo();
            ProcessCatchingKey();
        }

        private void OnExitInteractionZone()
        {
            StopCatchingKey();
        }

        private async void ProcessCatchingKey()
        {
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
        }

        protected abstract void Interact();
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            InteractionZone ??= GetComponentInChildren<InteractionZone>();
            _interactableUIView ??= GetComponentInChildren<InteractableUIView>();
        }

#endif
    }
}
