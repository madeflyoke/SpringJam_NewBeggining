using System;
using UnityEngine;

namespace Interactables.Interactors
{
    public class ConnectorPoint : MonoBehaviour
    {
        [SerializeField] private FixedJoint _connectionJoint;
        private bool _connected;
        private Rigidbody _connectedRb;
        
        public void Connect(Rigidbody rb)
        {
            _connectedRb = rb;
            _connectionJoint.connectedBody = _connectedRb;
            _connected = true;
        }

        public void Release()
        {
            if (_connected)
            {
                _connectionJoint.connectedBody = null;
                _connected = false;
            }
        }
    }
}
