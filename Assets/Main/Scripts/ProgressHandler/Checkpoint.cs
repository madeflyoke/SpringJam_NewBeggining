using System;
using Player;
using UnityEngine;

namespace Main.Scripts.ProgressHandler
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
