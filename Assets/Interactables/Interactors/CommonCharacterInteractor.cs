using Interactables.Enums;
using Interactables.Interfaces;
using Player;
using UnityEngine;

namespace Interactables.Interactors
{
    public class CommonCharacterInteractor : MonoBehaviour, IInteractor
    {
        public IInteractable CurrentInteractable { get; set; }
        public bool IsActive { get; private set; }

        [field: SerializeField] public ConnectorPoint ConnectorPoint { get; private set; }
        [field: SerializeField] public InteractorType InteractorType { get; private set; }

        [field: SerializeField] public Character _relatedCharacter;
        [SerializeField] private Collider _interactionCol;

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            _interactionCol.enabled = isActive;
        }
        
        private void TryReleaseInteraction()
        {
            if (CurrentInteractable!=null)
            {
                CurrentInteractable.ReleaseInteraction();
                CurrentInteractable = null;
            }
        }

        public void ResetInteractor()
        {
            TryReleaseInteraction();
            ConnectorPoint.Release();
        }
    }
}
