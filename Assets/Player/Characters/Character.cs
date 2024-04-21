using System;
using Interactables.Interactors;
using Player.Characters;
using UnityEngine;
using UniRx;

namespace Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterType type; 
        [SerializeField] private CharacterMovementComponent movementComponent;
        [SerializeField] private CommonCharacterInteractor commonCharacterInteractor;
        public CharacterType Type=>type;
        public CharacterMovementComponent MovementComponent=>movementComponent;
        public bool IsSelected { get; private set; }
        private IDisposable _groundCheckerDisposable;
        
        private void OnEnable()
        {
            _groundCheckerDisposable = movementComponent.ObserveEveryValueChanged(x => x.IsGrounded)
                .Subscribe(SetInteractorActive);
        }

        private void OnDisable()
        {
            _groundCheckerDisposable?.Dispose();
        }

        public void SetSelected(bool isSelected)
        {
            IsSelected = isSelected;
            SetInteractorActive(isSelected);
        }

        public void SetInteractorActive(bool isActive)
        {
            commonCharacterInteractor.SetActive(isActive);
            if (isActive==false)
            {
                commonCharacterInteractor.ResetInteractor();
            }
        }
    }

    public enum CharacterType
    {
        DEFAULT,
        Strongman,
        Trickster
    }
}
