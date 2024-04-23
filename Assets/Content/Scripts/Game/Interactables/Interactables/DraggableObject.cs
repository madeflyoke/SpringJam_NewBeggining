using System;
using Interactables;
using Interactables.Interactors;
using UnityEngine;

namespace Content.Scripts.Game.Interactables.Interactables
{
    public class DraggableObject : BaseInteractableObject
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private CapsuleCollider _physicCollider;
        private bool _isDragging;
        private Vector3 _yVelocity;
        private float _defaultZPos;

        protected override void Awake()
        {
            base.Awake();
            _defaultZPos = transform.position.z;
        }

        private void Update()
        {
            if (_controller.isGrounded==false)
            {
                _yVelocity.y += -15f * Time.deltaTime;
                
                _controller.Move(_yVelocity);
            }
            else if (_yVelocity.y<0)
            {
                _yVelocity.y = -2f;
            }
            
            var diff = _defaultZPos - transform.position.z;
            _controller.Move(Vector3.forward * diff);
        }

        protected override void TryInteract()
        {
            if (CurrentInteractor is CommonCharacterInteractor interactor)
            {
                if (_isDragging==false)
                {
                    SetDragging();
                    interactor.ConnectorPoint.Connect(_controller);
                }
                else
                {
                    interactor.ConnectorPoint.Release();
                    Release();
                }
            }
        }
        
        private void SetDragging()
        {
            InteractionZone.Disable(false);
            _isDragging = true;
        }

        private void Release()
        {
            _isDragging = false;
            InteractionZone.Enable();
        }
        
        protected override void OnExitInteractionZone()
        {
            if (_isDragging)
            {
                Release();
            }
            base.OnExitInteractionZone();
        }

        #if UNITY_EDITOR
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _physicCollider.center = _controller.center;
            _physicCollider.radius = _controller.radius+0.05f;
            _physicCollider.height = _controller.height;
        }
        
        #endif
    }
}
