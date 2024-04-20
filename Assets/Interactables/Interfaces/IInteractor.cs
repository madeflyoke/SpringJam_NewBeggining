using Interactables.Enums;
using UnityEngine;

namespace Interactables.Interfaces
{
   public interface IInteractor
   {
      public Transform SelfTransform { get; }

      public InteractorType InteractorType { get; }
   }
}
