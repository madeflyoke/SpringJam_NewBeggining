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
        
        public event Action<IInteractor> EnterInteractionZone;
        public event Action ExitInteractionZone;
        
        [SerializeField] private List<InteractorType> _allowedInteractors;
        private bool _inZone;

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
            if (_inZone)
            {
                return;
            }

            if (other.TryGetComponent(out IInteractor interactor)&&ValidateInteractable(interactor.InteractorType))
            {
                EnterInteractionZone?.Invoke(interactor);
                CurrentInteractor = interactor;
                _inZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_inZone)
            {
                return;
            }
            
            OnExit();
        }

        private void OnExit()
        {
            ExitInteractionZone?.Invoke();
            CurrentInteractor = null;
            _inZone = false;
        }
        
        private bool ValidateInteractable(InteractorType interactorType)
        {
            return _allowedInteractors.Contains(interactorType);
        }
    }
}
