using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Content.Scripts.Game.Interactables.Interactors
{
    public class ConnectorPoint : MonoBehaviour
    {
        public event Action ConnectionBrokeEvent;
        
        private bool _isConnected;
        private CancellationTokenSource _cts;
        private float _originalDistance;
        private CharacterController _connectedController;
        
        public void Connect(CharacterController controller)
        {
            if (_isConnected == false)
            {
                _connectedController = controller;
                _isConnected = true;
                ProcessConnecting();
            }
        }

        public void Release()
        {
            if (_isConnected)
            {
                _connectedController = null;
                _isConnected = false;
                _cts?.Cancel();
            }
        }

        public void TrySynchronizeConnected(Vector3 dir)
        {
            if (_isConnected)
            {
                _connectedController.Move(dir);
            }
        }

        private async void ProcessConnecting()
        {
            var originalDistance = _connectedController.transform.position.x - transform.position.x;
            _cts = new CancellationTokenSource();
            while ((_connectedController.transform.position.x - transform.position.x) < (originalDistance + 2f))
            {
                var isCanceled = await UniTask.Yield(_cts.Token).SuppressCancellationThrow();
                if (isCanceled)
                {
                    return;
                }
            }
            ConnectionBrokeEvent?.Invoke();
        }

        private void OnDisable()
        {
            _cts?.Cancel();
        }
    }
}
