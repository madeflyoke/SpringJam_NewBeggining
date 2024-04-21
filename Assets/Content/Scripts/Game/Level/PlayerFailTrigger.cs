using System;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class PlayerFailTrigger : MonoBehaviour
    {
        public event Action OnPlayerFail;

        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<Character>())
                OnPlayerFail?.Invoke();
        }
    }
}
