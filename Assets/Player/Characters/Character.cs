using System;
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
        [SerializeField] private CommonCharacterInteractor commonCharacterInteractor;
        public CharacterType Type=>type;
        public CharacterMovementComponent MovementComponent=>movementComponent;
        public bool IsSelected { get; set; }
        

        public void ResetInteractor()
        {
            commonCharacterInteractor.ResetInteractor();
        }
    }

    public enum CharacterType
    {
        DEFAULT,
        Strongman,
        Trickster
    }
}
