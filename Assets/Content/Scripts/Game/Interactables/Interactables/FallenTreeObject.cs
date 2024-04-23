using Content.Audio;
using DG.Tweening;
using EasyButtons;
using UnityEngine;

namespace Interactables
{
    public class FallenTreeObject : BaseInteractableObject
    {
        [SerializeField] private Transform _bottomPivot;
        [SerializeField] private bool _backwardFallSide;
        [SerializeField] private float _speed;
        
        [Button]
        protected override void TryInteract()
        {
            InteractionZone.Disable(true);
            KeyCatcher.Disable();
            SoundController.Instance?.PlayClip(SoundType.INTERACT, customVolume:0.1f);

            var finalRot = (_backwardFallSide ? transform.forward : -transform.forward) * 90f;
            var addedRot = finalRot + _bottomPivot.eulerAngles;
            _bottomPivot.DORotate(addedRot, _speed).SetEase(Ease.OutBounce,-1);
        }
    }
}
