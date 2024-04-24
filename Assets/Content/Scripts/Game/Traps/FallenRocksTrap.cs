using System;
using Content.Audio;
using Content.Scripts.Game.Level;
using Content.Scripts.Game.Player.Characters;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace SpringJam.Game.Traps
{
    public class FallenRocksTrap : MonoBehaviour
    {
        [Inject] private LevelLauncher _levelLauncher;
        
        [SerializeField] private BoxCollider fallenTrigger;
        [SerializeField] private Rigidbody rock;
        [SerializeField] private float force;
        [SerializeField] private Transform indicatorSprite;
        [SerializeField] private float _rockDistance;
        [SerializeField] private ParticleSystem _rockParticles;
        [SerializeField] private FallenRockFailTrigger _fallenRockFailTrigger;

        private Vector3 rockStartPosition;
        private bool alreadyFall;
        private IDisposable _indicatorDisposable;

        private void Awake()
        {
            rockStartPosition = rock.transform.position;
            rock.gameObject.SetActive(false);
            indicatorSprite.gameObject.SetActive(false);

            fallenTrigger.OnTriggerEnterAsObservable()
                .Subscribe(OnFallenTriggerEnter)
                .AddTo(this);
            
            _fallenRockFailTrigger.OnTriggerCaught += OnPlayerFail;
            _levelLauncher.OnPlayerSpawn += ResetTrap;
        }

        private void OnDestroy()
        {
            _fallenRockFailTrigger.OnTriggerCaught -= OnPlayerFail;
            _levelLauncher.OnPlayerSpawn -= ResetTrap;
        }

        private void OnPlayerFail()
        {
            _indicatorDisposable?.Dispose();
            OnLanding();
            _levelLauncher.OnPlayerFail?.Invoke();
        }
        
        private void ResetTrap()
        {
            alreadyFall = false;

            indicatorSprite.gameObject.SetActive(false);
            rock.gameObject.SetActive(false);
            rock.velocity = Vector3.zero;

            _rockParticles.transform.SetParent(rock.transform);
            rock.transform.position = rockStartPosition;
        }
        
        private void OnFallenTriggerEnter(Collider collider)
        {
            if (alreadyFall)
            {
                return;
            }

            if (IsCharacterEnterIn(collider.gameObject) == false)
            {
                return;
            }

            alreadyFall = true;

            rock.gameObject.SetActive(true);
            rock.AddForce(Vector3.down*force, ForceMode.Impulse);
            
            RunIndicator();
        }

        private void RunIndicator()
        {
            var defaultScale = indicatorSprite.transform.localScale;
            indicatorSprite.transform.localScale =Vector3.zero;
            indicatorSprite.gameObject.SetActive(true);

            var startDistance = Mathf.Abs(indicatorSprite.transform.position.y - (rock.position.y - 0.5f)) + 0.01f;

            _indicatorDisposable = indicatorSprite.UpdateAsObservable().SkipWhile(_=>rock.velocity.magnitude==0)
                .TakeWhile(_=>rock.velocity.magnitude>0.5f).Subscribe(x =>
                {
                    var distance = Mathf.Abs(indicatorSprite.transform.position.y - (rock.position.y-0.5f))+0.01f;
                    
                    float t = Mathf.Clamp01((distance - startDistance) / (0.5f - startDistance));

                    float easedT = 1f - Mathf.Pow(1f - t, 3f);
                    
                    indicatorSprite.transform.localScale = Vector3.Lerp(Vector3.zero, defaultScale,
                        easedT);
                    
                }, onCompleted: ()=>
                {
                    OnLanding();
                }).AddTo(this);
        }

        private void OnLanding()
        {
            indicatorSprite.gameObject.SetActive(false);
            rock.gameObject.SetActive(false);
            rock.velocity = Vector3.zero;
            _rockParticles.transform.SetParent(transform);
            _rockParticles.Play();
            SoundController.Instance?.PlayClip(SoundType.STONE_HIT, customVolume: 0.06f, customPitch: 1.1f);
        }
        

        private bool IsCharacterEnterIn(GameObject player)
        {
            return player.TryGetComponent(out Character _);
        }
        
#if UNITY_EDITOR
        
        private void OnValidate()
        {
            var launcher = FindObjectOfType<LevelLauncher>();

            if (launcher!=null)
            {
                var indicatorPos = indicatorSprite.transform.position;
                indicatorPos.z = launcher.StartPoint.position.z;
                indicatorSprite.transform.position = indicatorPos;

                indicatorPos.y += _rockDistance;
                rock.transform.position = indicatorPos;
            }
        }

#endif
    }
}