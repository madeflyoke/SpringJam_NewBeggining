using System;
using Content.Scripts.Game.Level;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;
using Zenject;

namespace Content.Scripts.Game.ProgressHandler
{
    public class Checkpoint : MonoBehaviour
    {
        [Inject] private LevelLauncher _levelLauncher;
        
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private ParticleSystem _activeParticles;
        [SerializeField] private CharacterType _targetActivatorType;
        [SerializeField] private AudioSource _fireSfx;
        
        public Vector3 RespawnPoint => respawnPoint.position;
        public static event Action<Checkpoint> OnPlayerReach;
        public bool Reached { get; private set; }

        private void Start()
        {
            var diff = transform.position.z - _levelLauncher.StartPoint.position.z;
            transform.position -= Vector3.forward*diff;
            
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
                        _fireSfx.Play();
                        OnPlayerReach?.Invoke(this);
                    }
                }
            
        }
    }
}
