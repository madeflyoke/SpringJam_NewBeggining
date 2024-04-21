using System;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;

namespace Content.Scripts.Game.ProgressHandler
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform respawnPoint;
        public Vector3 RespawnPoint => respawnPoint.position;
        public event Action<Checkpoint> OnPlayerReach;
        public bool Reached { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if(!Reached)
                if (other.gameObject.TryGetComponent(out Character character))
                {
                    Reached = true;
                    OnPlayerReach?.Invoke(this);
                }
            
        }
    }
}
