using UnityEngine;

namespace Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterType type; 
        [SerializeField] private CharacterMovementComponent movementComponent; 
        [SerializeField] private CharacterInteractor interactor;
        public CharacterType Type=>type;
        public CharacterMovementComponent MovementComponent=>movementComponent;
        public CharacterInteractor Interactor=>interactor;
    }

    public enum CharacterType
    {
        Strongman,
        Trickster
    }
}
