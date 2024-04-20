using Interactables.Enums;
using Interactables.Interfaces;
using UnityEngine;

namespace Player
{
    public class Character : MonoBehaviour, IMovableInteractor
    {
        public Vector3 CurrentPosition => transform.position;
        public Vector3 CurrentVelocity => movementComponent.ControllerVelocity;
        [field: SerializeField] public InteractorType InteractorType { get; private set; }
        
        [SerializeField] private CharacterType type; 
        [SerializeField] private CharacterMovementComponent movementComponent; 
        public CharacterType Type=>type;
        public CharacterMovementComponent MovementComponent=>movementComponent;
    }

    public enum CharacterType
    {
        DEFAULT,
        Strongman,
        Trickster
    }
}
