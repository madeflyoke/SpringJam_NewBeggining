using Content.Scripts.Game.Interactables.Interactors;
using Interactables.Enums;
using Interactables.Interfaces;
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

        private void OnEnable()
        {
            ConnectorPoint.ConnectionBrokeEvent += ResetInteractor;
        }

        private void OnDisable()
        {
            ConnectorPoint.ConnectionBrokeEvent -= ResetInteractor;
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
