using UnityEngine;

namespace Content.Scripts.Game
{
    public class CameraAttachments : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _snowParticles;
        [SerializeField] private ParticleSystem _fliesParticles;

        public void SetSnowParticles(bool isActive)
        {
            if (isActive)
            {
                _snowParticles.Play();
            }
            else
            {
                _snowParticles.Stop();
            }
        }

        public void SetFirefliesParticles(bool isActive)
        {
            if (isActive)
            {
                _fliesParticles.Play();
            }
            else
            {
                _fliesParticles.Stop();
            }
        }
    }
}
