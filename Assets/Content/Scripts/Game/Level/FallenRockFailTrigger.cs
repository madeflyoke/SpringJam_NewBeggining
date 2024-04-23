using System;
using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class FallenRockFailTrigger : PlayerFailTrigger
    {
        public event Action OnTriggerCaught;

        protected override void TriggerEntered(Collider other)
        {
            if (transform.position.y> other.bounds.max.y)
            {
                OnTriggerCaught?.Invoke();
            }
        }
    }
}