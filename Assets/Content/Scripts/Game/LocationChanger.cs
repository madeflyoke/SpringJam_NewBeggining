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
        [SerializeField] private Color _forestFogColor;
        [SerializeField] private Color _caveFogColor;

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
            RenderSettings.fogColor = _forestFogColor;
            S_currentLocationType = LocationPartType.FOREST;
        }

        private void SetCaveType()
        {
            _snowParticles.Stop();
            _fliesParticles.Play();
            RenderSettings.fogColor = _caveFogColor;
            S_currentLocationType = LocationPartType.CAVE;
        }
    }
}
