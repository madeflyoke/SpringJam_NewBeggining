using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Interactables
{
    public class DraggableObject : BaseInteractableObject
    {
        [SerializeField] private float _dragLimitDistance =10f;
        [SerializeField] private Rigidbody _rb;
        [Header("Automatic")]
        [SerializeField] private float _breakDistance;
        
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

        private void Update()
        {
            if (InteractionZone.CurrentInteractor!=null)
            {
                Debug.LogWarning(Vector3.Distance(InteractionZone.CurrentInteractor.CurrentPosition, _rb.position));

            }
        }

        private async void Drag()
        {
            _cts = new CancellationTokenSource();
            
            while (true)
            {
                var isCanceled = await UniTask.Yield(_cts.Token).SuppressCancellationThrow();
                if (isCanceled ||
                    Vector3.Distance(InteractionZone.CurrentInteractor.CurrentPosition, _rb.position)>_breakDistance)
                {
                    Release();
                    return;
                }

                if (Vector3.Distance(_rb.position, _originalPoint)>=_dragLimitDistance)
                {
                    var clampedPos = _rb.position;
                    clampedPos.x -= 0.1f * Mathf.Sign(Vector3.Dot(_originalPoint.normalized - transform.position.normalized, Vector3.left));
                    _rb.position = clampedPos;
                    Release();
                    return;
                }

                _rb.velocity = InteractionZone.CurrentInteractor.CurrentVelocity;
            }
        }

        private void Release()
        {
            _cts?.Cancel();
            _rb.velocity = Vector3.zero;
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
                Handles.DrawLine(_originalPoint-transform.right*_dragLimitDistance, _originalPoint+transform.right*_dragLimitDistance);
            }
            else
            {
                Handles.DrawLine(transform.position-transform.right*_dragLimitDistance, transform.position+transform.right*_dragLimitDistance);
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            _breakDistance = InteractionZone.GetComponent<Collider>().bounds.size.x / 2 + 1.5f;
        }

#endif

        protected override void OnDisable()
        {
            base.OnDisable();
            _cts?.Cancel();
        }
    }
}
