using System;
using Interactables.Interfaces;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(InteractionKeyCatcher))]
    public abstract class BaseInteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] protected InteractionZone InteractionZone;
        [SerializeField] protected InteractableUIView InteractableUIView;
        [SerializeField] protected InteractionKeyCatcher KeyCatcher;
        protected IInteractor CurrentInteractor;
        
        protected virtual void Awake()
        {
            InteractionZone.Init(this);   
        }

        public void SetInteractor(IInteractor interactor)
        {
            CurrentInteractor = interactor;
        }

        protected virtual void OnEnable()
        {
            InteractionZone.EnterInteractionZone += OnEnterInteractionZone;
            InteractionZone.ExitInteractionZone += OnExitInteractionZone;
        }

        protected virtual void OnDisable()
        {
            InteractionZone.EnterInteractionZone -= OnEnterInteractionZone;
            InteractionZone.ExitInteractionZone -= OnExitInteractionZone;
        }

        private void OnKeyCaught()
        {
            InteractableUIView.HideButtonInfo();
            TryInteract();
        }
        
        private void OnEnterInteractionZone()
        {
            InteractableUIView.ShowButtonInfo();
            KeyCatcher.OnKeyPressed += OnKeyCaught;
        }

        protected virtual void OnExitInteractionZone()
        {
            InteractableUIView.HideButtonInfo();
            KeyCatcher.OnKeyPressed -= OnKeyCaught;
            CurrentInteractor = null;
        }

        protected abstract void TryInteract();
        
        public void ReleaseInteraction()
        {
            InteractionZone.OnExit();
        }

#if UNITY_EDITOR

        protected virtual void OnValidate()
        {
            InteractionZone ??= GetComponentInChildren<InteractionZone>();
            InteractableUIView ??= GetComponentInChildren<InteractableUIView>();
            KeyCatcher ??= GetComponentInChildren<InteractionKeyCatcher>();
        }

#endif

    }
}
