using Interactables.Interactors;
using UnityEditor;
using UnityEngine;

namespace Interactables
{
    public class DraggableObject : BaseInteractableObject
    {
        [SerializeField] private float _dragLimitDistance =10f;
        [SerializeField] private Rigidbody _rb;
        private Vector3 _originalPoint;
        private bool _isDragging;

        protected override void Awake()
        {
            base.Awake();
            _originalPoint = transform.position;
            Freeze(true);
        }

        private void Freeze(bool isFrozen)
        {
            if (isFrozen)
            {
                _rb.constraints =  RigidbodyConstraints.FreezePositionX |
                                   RigidbodyConstraints.FreezePositionZ |
                                   RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                _rb.constraints =  _rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                                     RigidbodyConstraints.FreezeRotation;
            }
        }
        
        protected override void TryInteract()
        {
            if (CurrentInteractor is CommonInteractor interactor)
            {
                if (_isDragging==false)
                {
                    SetDragging(interactor);
                }
                else
                {
                    Release(interactor);
                }
            }
        }
        
        private void SetDragging(CommonInteractor interactor)
        {
            InteractionZone.Disable(false);
            interactor.ConnectorPoint.Connect(_rb);
            Freeze(false);
            _isDragging = true;
        }

        private void Release(CommonInteractor interactor)
        {
            _isDragging = false;
            Freeze(true);
            interactor.ConnectorPoint.Release();
            InteractionZone.Enable();
            InteractableUIView.ShowButtonInfo();
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
#endif
    }
}
