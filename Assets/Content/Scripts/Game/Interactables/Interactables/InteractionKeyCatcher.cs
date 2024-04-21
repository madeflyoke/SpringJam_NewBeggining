using System;
using UnityEngine;

namespace Interactables
{
    public class InteractionKeyCatcher : MonoBehaviour
    {
        public event Action OnKeyPressed;

        public void Disable()
        {
            enabled = false;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnKeyPressed?.Invoke();
            }
        }
    }
}