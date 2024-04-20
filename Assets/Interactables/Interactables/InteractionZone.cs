using System;
using System.Collections.Generic;
using Interactables.Enums;
using Interactables.Interfaces;
using UnityEngine;

namespace Interactables
{
    public class InteractionZone : MonoBehaviour
    {

        public event Action EnterInteractionZone;
        public event Action ExitInteractionZone;

        [SerializeField] private List<InteractorType> _allowedInteractors;
        private IInteractable _relatedInteractable;
        private bool _zoneInteracting;

        public void Init(IInteractable relatedInteractable)
        {
            _relatedInteractable = relatedInteractable;
        }

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
            if (gameObject!=null)
            {
                gameObject.SetActive(true);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_zoneInteracting)
            {
                return;
            }
            
            if (other.TryGetComponent(out IInteractor interactor) && ValidateInteractable(interactor.InteractorType))
            {
                _relatedInteractable.SetInteractor(interactor);
                EnterInteractionZone?.Invoke();
                _zoneInteracting = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_zoneInteracting==false)
            {
                return;
            }
            
            OnExit();
        }

        private void OnExit()
        {
            ExitInteractionZone?.Invoke();
            _zoneInteracting = false;
        }
        
        private bool ValidateInteractable(InteractorType interactorType)
        {
            return _allowedInteractors.Contains(interactorType);
        }
        
    }
}
