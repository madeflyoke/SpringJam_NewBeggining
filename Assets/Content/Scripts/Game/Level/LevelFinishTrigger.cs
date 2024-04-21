using System;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class LevelFinishTrigger : MonoBehaviour
    {
        public event Action OnPlayerWin;

        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<Character>())
                OnPlayerWin?.Invoke();
        }
    }
}
