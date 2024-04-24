using Content.Scripts.Game.Player.Characters;
using UnityEngine;

namespace Content.Scripts.Game
{
    public enum LocationPartType
    {
        NONE= 0,
        FOREST =1,
        CAVE = 2,
    }
    
    public class LocationChanger : MonoBehaviour
    {
        public static LocationPartType S_currentLocationType;

        [SerializeField] private LocationPartType _locationType;
        [SerializeField] private ParticleSystem _snowParticles;
        [SerializeField] private ParticleSystem _fliesParticles;
        [SerializeField] private AudioSource _windAudio;

        private void Awake()
        {
            SetForestType();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject==UnityEngine.Camera.main.gameObject)
            {
                if (S_currentLocationType==_locationType)
                {
                    return;
                }
                if (_locationType==LocationPartType.FOREST)
                {
                    SetForestType();
                }
                else
                {
                    SetCaveType();
                }
            }
        }

        private void SetForestType()
        {
            _snowParticles.Play();
            _fliesParticles.Stop();
            _windAudio.Play();
            S_currentLocationType = LocationPartType.FOREST;
        }

        private void SetCaveType()
        {
            _snowParticles.Stop();
            _fliesParticles.Play();
            _windAudio.Stop();
            S_currentLocationType = LocationPartType.CAVE;
        }
    }
}
