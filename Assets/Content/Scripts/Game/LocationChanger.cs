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

        private void Awake()
        {
            SetForestType();
        }
        
        private void OnTriggerEnter(Collider other)
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

        private void SetForestType()
        {
            _snowParticles.Play();
            _fliesParticles.Stop();
            S_currentLocationType = LocationPartType.FOREST;
        }

        private void SetCaveType()
        {
            _snowParticles.Stop();
            _fliesParticles.Play();
            S_currentLocationType = LocationPartType.CAVE;
        }
    }
}
