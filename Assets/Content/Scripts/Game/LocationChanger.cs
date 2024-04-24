using UniRx;
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

        [SerializeField] private CameraAttachments _cameraAttachments;
        [SerializeField] private AudioSource _windAudio;
        private Transform _camTransform;
        private Vector2 _changerSafeBounds;

        private bool _isLefter => _camTransform.position.x<=_changerSafeBounds.x;
        private bool _isRighter =>_camTransform.position.x>_changerSafeBounds.y;
        
        private void Awake()
        {
            _camTransform = _cameraAttachments.transform;
            _changerSafeBounds = new Vector2(transform.position.x - 2f, transform.position.x + 2);
            SetForestType();

            this.ObserveEveryValueChanged(x => _isLefter).Where(x=>!x).Subscribe(_ =>
            {
                SetCaveType();
            }).AddTo(this);
            
            this.ObserveEveryValueChanged(x => _isRighter).Where(x=>!x).Subscribe(_ =>
            {
                SetForestType();
            }).AddTo(this);
        }

        private void SetForestType()
        {
            if (S_currentLocationType!=LocationPartType.FOREST)
            {
                S_currentLocationType = LocationPartType.FOREST;
                _windAudio.Play();
                SetParticles();
            }
        }

        private void SetCaveType()
        {
            if (S_currentLocationType!=LocationPartType.CAVE)
            {
                S_currentLocationType = LocationPartType.CAVE;
                _windAudio.Stop();
                SetParticles();
            }
        }

        private void SetParticles()
        {
            _cameraAttachments.SetSnowParticles(S_currentLocationType==LocationPartType.FOREST);
            _cameraAttachments.SetFirefliesParticles(S_currentLocationType!=LocationPartType.FOREST);
        }
        
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            _cameraAttachments = FindObjectOfType<CameraAttachments>();
        }

#endif
    }
}
