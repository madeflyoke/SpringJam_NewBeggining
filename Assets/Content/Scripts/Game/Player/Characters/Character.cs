using System;
using Content.Audio;
using Interactables.Interactors;
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
        private IDisposable _groundCheckerDisposable;
        
        private void OnEnable()
        {
            _groundCheckerDisposable = movementComponent
                .ObserveEveryValueChanged(x => x.IsGrounded)
                .Subscribe(x=>
                {
                    SetInteractorActive(x);
                    if (x)
                    {
                        OnLanded();
                    }
                });
        }

        private void OnDisable()
        {
            _groundCheckerDisposable?.Dispose();
        }

        private void OnLanded()
        {
            SoundController.Instance?.PlayClip(LocationChanger.S_currentLocationType==LocationPartType.FOREST? SoundType.STEP_SNOW : SoundType.STEP_ROCK, isRandom:true);
        }

        public void SetSelected(bool isSelected, bool isCombined=false)
        {
            SetInteractorActive(isSelected && !isCombined);
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
