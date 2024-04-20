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
            
            var finalRot = (_backwardFallSide ? -transform.right : transform.right) * 90f;
            _bottomPivot.DORotate(finalRot,_speed).SetEase(Ease.InQuad);
        }
    }
}