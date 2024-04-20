using System;
using System.Collections.Generic;
using Interactables.Enums;
using Interactables.Interfaces;
using UnityEngine;

namespace Interactables
{
    public class InteractionZone : MonoBehaviour
    {
        public IInteractor CurrentInteractor { get; private set; }
        
        public event Action EnterInteractionZone;
        public event Action ExitInteractionZone;
        
        [SerializeField] private List<InteractorType> _allowedInteractors;

        public void Disable(bool totally)
        {
            if (totally)
            {
                OnExit();
            }
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.LogWarning("ENTERD");
            if (CurrentInteractor!=null)
            {
                return;
            }

            if (other.TryGetComponent(out IInteractor interactor)&&ValidateInteractable(interactor.InteractorType))
            {
                CurrentInteractor = interactor;
                EnterInteractionZone?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (CurrentInteractor==null)
            {
                return;
            }
            
            OnExit();
        }

        private void OnExit()
        {
            ExitInteractionZone?.Invoke();
            CurrentInteractor = null;
        }
        
        private bool ValidateInteractable(InteractorType interactorType)
        {
            return _allowedInteractors.Contains(interactorType);
        }
    }
}
