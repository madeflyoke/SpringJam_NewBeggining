using Interactables.Enums;
using Interactables.Interfaces;
using UnityEngine;

namespace Interactables.Interactors
{
    public class CommonInteractor : MonoBehaviour, IInteractor
    {
        public IInteractable CurrentInteractable { get; set; }

        [field: SerializeField] public ConnectorPoint ConnectorPoint { get; private set; }
        [field: SerializeField] public InteractorType InteractorType { get; private set; }

        private void TryReleaseInteraction()
        {
            if (CurrentInteractable!=null)
            {
                CurrentInteractable.ReleaseInteraction();
            }
        }

        public void ResetInteractor()
        {
            TryReleaseInteraction();
            ConnectorPoint.Release();
        }
    }
}
