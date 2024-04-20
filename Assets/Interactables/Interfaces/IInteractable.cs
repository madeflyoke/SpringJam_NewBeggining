namespace Interactables.Interfaces
{
    public interface IInteractable
    {
        public void ReleaseInteraction();
        public void SetInteractor(IInteractor interactor);
    }
}
