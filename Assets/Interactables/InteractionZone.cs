using System;
using System.Collections.Generic;
using Interactables.Enums;
using Interactables.Interfaces;
using UnityEngine;

namespace Interactables
{
    public class InteractionZone : MonoBehaviour
    {
        public event Action<bool> EnterInteraction;

        public event Action ExitInteraction;
        
        [SerializeField] private List<InteractorType> _allowedInteractors;
        private bool _inInteractionProcess;

        private void OnTriggerEnter(Collider other)
        {
            Debug.LogWarning("ENTERD");
            if (_inInteractionProcess)
            {
                return;
            }
            
            _inInteractionProcess = true;
            EnterInteraction?.Invoke(other.TryGetComponent(out IInteractor interactor) 
                                     && ValidateInteractable(interactor.InteractorType));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_inInteractionProcess)
            {
                return;
            }
            
            OnExit();
        }

        private void OnExit()
        {
            ExitInteraction?.Invoke();
            _inInteractionProcess = false;
        }
        
        public void Disable()
        {
            OnExit();
            gameObject.SetActive(false);
        }

        private bool ValidateInteractable(InteractorType interactorType)
        {
            return _allowedInteractors.Contains(interactorType);
        }
    }
}
