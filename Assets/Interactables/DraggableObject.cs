using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Interactables
{
    public class DraggableObject : BaseInteractableObject
    {
        [SerializeField] private float _breakDistance;
        [SerializeField] private Rigidbody _rb;
        private Vector3 _originalPoint;
        
        private bool _isDragging;
        private CancellationTokenSource _cts;

        private void Awake()
        {
            _originalPoint = transform.position;
        }

        protected override void Interact()
        {
            if (_isDragging==false)
            {
                InteractionZone.Disable(false);
                Drag();
                _isDragging = true;
            }
            else
            {
                Release();
            }
        }

        private async void Drag()
        {
            var interactorTr = InteractionZone.CurrentInteractor.SelfTransform;
            
            var distanceBetweenInteractor = interactorTr.position.x - transform.position.x;
            _cts = new CancellationTokenSource();
            
            while (true)
            {
                var isCanceled = await UniTask.Yield(_cts.Token).SuppressCancellationThrow();
                if (isCanceled)
                {
                    Release();
                    return;
                }
                
                var correctedPos = transform.position;
                correctedPos.x = interactorTr.position.x - distanceBetweenInteractor;
                
                if (Vector3.Distance(correctedPos, _originalPoint)>=_breakDistance)
                {
                    Release();
                    return;
                }
                
                _rb.MovePosition(correctedPos);
            }
        }

        private void Release()
        {
            _cts?.Cancel();
            InteractionZone.Enable();
            OnEnterInteractionZone();
            _isDragging = false;
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {            
            Handles.color = Color.yellow;
            if (Application.isPlaying)
            {
                Handles.DrawLine(_originalPoint-transform.right*_breakDistance, _originalPoint+transform.right*_breakDistance);
            }
            else
            {
                Handles.DrawLine(transform.position-transform.right*_breakDistance, transform.position+transform.right*_breakDistance);
            }
        }

#endif

        protected override void OnDisable()
        {
            base.OnDisable();
            _cts?.Cancel();
        }
    }
}
