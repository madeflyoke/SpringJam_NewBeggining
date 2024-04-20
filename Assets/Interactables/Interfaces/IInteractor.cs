using Interactables.Enums;

namespace Interactables.Interfaces
{
   public interface IInteractor
   {
      public IInteractable CurrentInteractable { get; set; }
      public InteractorType InteractorType { get; }
   }
}
