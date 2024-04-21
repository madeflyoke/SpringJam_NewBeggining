using System;
using DG.Tweening;
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
        private static LocationPartType s_currentLocationType;

        [SerializeField] private LocationPartType _locationType;
        [SerializeField] private ParticleSystem _snowParticles;
        [SerializeField] private Color _forestFogColor;
        [SerializeField] private Color _caveFogColor;

        private void Awake()
        {
            SetForestType();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (s_currentLocationType==_locationType)
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
            RenderSettings.fogColor = _forestFogColor;
            s_currentLocationType = LocationPartType.FOREST;
        }

        private void SetCaveType()
        {
            _snowParticles.Stop();
            RenderSettings.fogColor = _caveFogColor;
            s_currentLocationType = LocationPartType.CAVE;
        }
    }
}
