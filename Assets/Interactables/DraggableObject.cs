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
            }
            else
            {
                InteractionZone.Enable();
                Release();
            }
            
            _isDragging = !_isDragging;
        }

        private async void Drag()
        {
            var interactorTr = InteractionZone.CurrentInteractor.SelfTransform;
            
            var distanceBetweenInteractor = interactorTr.position.x - transform.position.x;
            _cts = new CancellationTokenSource();
            
            while (Vector3.Distance(transform.position, _originalPoint)<_breakDistance)
            {
                var correctedPos = transform.position;
                correctedPos.x = interactorTr.position.x-distanceBetweenInteractor;
                
                Physics.Simulate(Time.fixedDeltaTime);
                _rb.MovePosition(correctedPos);
                
                var isCanceled= await UniTask.Yield(_cts.Token).SuppressCancellationThrow();
                if (isCanceled)
                {
                    return;
                }
            }
        }

        private void Release()
        {
            _cts?.Cancel();
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.yellow;
            Handles.DrawLine(transform.position, transform.position+transform.right*_breakDistance);
            Handles.DrawLine(transform.position, transform.position-transform.right*_breakDistance);
        }

#endif

        protected override void OnDisable()
        {
            base.OnDisable();
            _cts?.Cancel();
        }
    }
}
