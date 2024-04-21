using System;
using Interactables.Interactors;
using SpringJam.Game.Character;
using UniRx;
using UnityEngine;

namespace Content.Scripts.Game.Player.Characters
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
            _groundCheckerDisposable = movementComponent
                .ObserveEveryValueChanged(x => x.IsGrounded)
                .Subscribe(SetInteractorActive);
        }

        private void OnDisable()
        {
            _groundCheckerDisposable?.Dispose();
        }

        public void SetSelected(bool isSelected, bool isCombined=false)
        {
            IsSelected = isSelected;
            SetInteractorActive(isCombined?!isSelected:isSelected);
        }

        private void SetInteractorActive(bool isActive)
        {
            commonCharacterInteractor.SetActive(isActive);
        }
    }

    public enum CharacterType
    {
        DEFAULT,
        Strongman,
        Trickster
    }
}
