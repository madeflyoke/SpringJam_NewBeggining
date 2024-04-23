using System;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class PlayerFailTrigger : MonoBehaviour
    {
        public static event Action OnPlayerFail;
        public void OnTriggerEnter(Collider other)
        {
            if(ValidateCondition(other))
                OnPlayerFail?.Invoke();
        }

        protected virtual bool ValidateCondition(Collider other)
        {
            return other.gameObject.GetComponent<Character>();
        }
    }
}
