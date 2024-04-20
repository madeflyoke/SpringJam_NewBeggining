using Interactables.Enums;
using Interactables.Interfaces;
using Player;
using UnityEngine;

namespace Interactables.Interactors
{
    public class CommonCharacterInteractor : MonoBehaviour, IInteractor
    {
        public IInteractable CurrentInteractable { get; set; }
        public bool IsActive => _relatedCharacter.IsSelected;

        [field: SerializeField] public ConnectorPoint ConnectorPoint { get; private set; }
        [field: SerializeField] public InteractorType InteractorType { get; private set; }

        [field: SerializeField] public Character _relatedCharacter;

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
