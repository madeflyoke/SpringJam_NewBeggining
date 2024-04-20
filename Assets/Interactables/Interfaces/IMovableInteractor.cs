using Interactables.Enums;
using UnityEngine;

namespace Interactables.Interfaces
{
   public interface IMovableInteractor
   {
      public Vector3 CurrentVelocity { get; }

      public InteractorType InteractorType { get; }
   }
}
