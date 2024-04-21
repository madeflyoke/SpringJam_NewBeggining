using System;
using Interactables.Enums;
using Interactables.Interfaces;
using Player;
using Player.Characters;
using UnityEngine;

namespace Interactables.Interactors
{
    public class CommonCharacterInteractor : MonoBehaviour, IInteractor
    {
        public IInteractable CurrentInteractable { get; set; }
        public bool IsActive { get; private set; }

        [field: SerializeField] public ConnectorPoint ConnectorPoint { get; private set; }
        [field: SerializeField] public InteractorType InteractorType { get; private set; }

        [SerializeField] private Collider _interactionCol;
        [SerializeField] private CharacterMovementComponent _characterMovementComponent;


        public void CallOnXMoveCharacter(Type caller, float xDistance)
        {
            if (caller==typeof(ConnectorPoint))
            {
                var newPos = _characterMovementComponent.transform.position;
                newPos.x -= xDistance;
                _characterMovementComponent.SetPosition(newPos);
            }
        }

        public void CallOnConnectorBreak()
        {
            ResetInteractor();
        }
        
        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            _interactionCol.enabled = isActive;
            if (isActive==false)
            {
                ResetInteractor();
            }
        }
        
        private void ResetInteractor()
        {
            TryReleaseInteraction();
            ConnectorPoint.Release();
        }
        
        private void TryReleaseInteraction()
        {
            if (CurrentInteractable!=null)
            {
                CurrentInteractable.ReleaseInteraction();
                CurrentInteractable = null;
            }
        }
    }
}
