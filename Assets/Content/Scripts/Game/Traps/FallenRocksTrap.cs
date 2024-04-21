using Content.Scripts.Game.Level;
using Content.Scripts.Game.Player.Characters;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SpringJam.Game.Traps
{
    public class FallenRocksTrap : MonoBehaviour
    {
        [SerializeField] private BoxCollider fallenTrigger;
        [SerializeField] private GameObject rock;

        private PlayerFailTrigger failTrigger;
        private Vector3 rockStartPosition;
        private bool alreadyFall;

        private void Awake()
        {
            failTrigger = rock.GetComponent<PlayerFailTrigger>();
            rockStartPosition = rock.transform.position;

            fallenTrigger.OnTriggerEnterAsObservable()
                .Subscribe(OnFallenTriggerEnter)
                .AddTo(this);

            failTrigger.OnPlayerFail += ResetTrap;
        }

        private void OnDestroy()
        {
            failTrigger.OnPlayerFail -= ResetTrap;
        }

        public void ResetTrap()
        {
            alreadyFall = false;
            rock.transform.position = rockStartPosition;

            rock.gameObject.SetActive(false);
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
        }

        private bool IsCharacterEnterIn(GameObject player)
        {
            return player.TryGetComponent(out Character _);
        }
    }
}