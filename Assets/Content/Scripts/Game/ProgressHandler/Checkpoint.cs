using System;
using Content.Scripts.Game.Level;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;

namespace Content.Scripts.Game.ProgressHandler
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private ParticleSystem _activeParticles;
        [SerializeField] private CharacterType _targetActivatorType;
        public Vector3 RespawnPoint => respawnPoint.position;
        public static event Action<Checkpoint> OnPlayerReach;
        public bool Reached { get; private set; }

        private void Start()
        {
            _activeParticles.gameObject.SetActive(false);
            _activeParticles.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!Reached)
                if (other.gameObject.TryGetComponent(out Character character))
                {
                    if (character.Type==_targetActivatorType)
                    {
                        Reached = true;
                        _activeParticles.gameObject.SetActive(true);
                        _activeParticles.Play();
                        OnPlayerReach?.Invoke(this);
                    }
                }
            
        }
        
#if UNITY_EDITOR

        [SerializeField] private Transform EDITOR_startpoint;
        
        private void OnValidate()
        {
            EDITOR_startpoint = FindObjectOfType<LevelLauncher>().StartPoint;
            
            var pos = transform.position;
            pos.z = EDITOR_startpoint.position.z;
            transform.position = pos;
        }

#endif
    }
}
