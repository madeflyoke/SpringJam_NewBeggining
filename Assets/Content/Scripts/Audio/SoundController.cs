using System;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Content.Audio
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance { get; private set; }
        public float SoundsVolume => _soundsVolume;
        
        [SerializeField] private AudioSource _audioSourcePrefab;
        [SerializeField, Range(0.01f,1f)] private float _soundsVolume;
        [SerializeField] private List<ClipBySoundType> _clips;
        [SerializeField] private AudioSource _mainMusicSource;
        [SerializeField] private List<AudioClip> _musicClips;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }
        
        public void PlayClip(SoundType soundType, float customVolume = 0f, float customPitch = 1f, bool isRandom = false)
        {
            var audioSource = LeanPool.Spawn(_audioSourcePrefab);
            var clips = _clips.FirstOrDefault(x => x.SoundType == soundType).Clips;
            
            audioSource.clip = clips[isRandom? Random.Range(0, clips.Count-1) : 0];
            audioSource.volume = customVolume!=0f? customVolume: _soundsVolume;
            audioSource.pitch = customPitch;
            LeanPool.Despawn(audioSource, audioSource.clip.length);
            audioSource.Play();
        }
        
        [Serializable]
        private struct ClipBySoundType
        {
            public SoundType SoundType;
            public List<AudioClip> Clips;
        }
    }
    
}
