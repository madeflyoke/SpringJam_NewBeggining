using Interactables.Enums;
using Interactables.Interactors;
using Interactables.Interfaces;
using Player.Characters;
using UnityEngine;

namespace Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterType type; 
        [SerializeField] private CharacterMovementComponent movementComponent;
        [SerializeField] private CommonInteractor commonInteractor;
        public CharacterType Type=>type;
        public CharacterMovementComponent MovementComponent=>movementComponent;

        public void ResetInteractor()
        {
            commonInteractor.ResetInteractor();
        }
    }

    public enum CharacterType
    {
        DEFAULT,
        Strongman,
        Trickster
    }
}
