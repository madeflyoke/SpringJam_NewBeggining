using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interactables.Interactors
{
    public class ConnectorPoint : MonoBehaviour
    {
        [SerializeField] private ConfigurableJoint _connectionJoint;
        [SerializeField] private CommonCharacterInteractor _interactor;
        private bool _isConnected;
        private Rigidbody _connectedRb;
        private CancellationTokenSource _cts;
        private float _originalDistance;

        public void Connect(Rigidbody rb)
        {
            if (_isConnected == false)
            {
                _connectedRb = rb;
                _connectionJoint.connectedBody = _connectedRb;
                _isConnected = true;
                CorrectCloseDistance();
                ValidateBreakingDistance();
            }
        }

        public void Release()
        {
            if (_isConnected)
            {
                _connectionJoint.connectedBody = null;
                _isConnected = false;
                _connectedRb = null;
                _cts?.Cancel();
            }
        }

        private void CorrectCloseDistance()
        {
            _interactor.CallOnXMoveCharacter(GetType(), 0.2f*Mathf.Sign(_connectedRb.position.x-transform.position.x));
            _originalDistance = _connectedRb.position.x - transform.position.x;
        }

        private async void ValidateBreakingDistance()
        {
            _cts = new CancellationTokenSource();
            while ((_connectedRb.position.x - transform.position.x) < (_originalDistance + 1f))
            {
                var isCanceled = await UniTask.Yield(_cts.Token).SuppressCancellationThrow();
                if (isCanceled)
                {
                    return;
                }
            }
            Debug.LogWarning("BREAK");
            _interactor.CallOnConnectorBreak();
        }

        private void OnDisable()
        {
            _cts?.Cancel();
        }
    }
}
