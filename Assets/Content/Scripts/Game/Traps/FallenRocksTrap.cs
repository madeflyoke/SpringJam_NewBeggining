using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SpringJam.Game.Traps
{
    public class FallenRocksTrap : MonoBehaviour
    {
        [SerializeField] private BoxCollider fallenTrigger;
        [SerializeField] private SphereCollider rockTrigger;
        [SerializeField] private GameObject rock;

        private Vector3 rockStartPosition;
        private bool alreadyFall;

        private void Awake()
        {
            rockStartPosition = rock.transform.position;

            fallenTrigger.OnTriggerEnterAsObservable()
                .Subscribe(OnFallenTriggerEnter)
                .AddTo(this);

            rockTrigger.OnCollisionEnterAsObservable()
                .Subscribe(OnRockCollisionEnter)
                .AddTo(this);
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

            alreadyFall = true;

            rock.gameObject.SetActive(true);
        }

        private void OnRockCollisionEnter(Collision collision)
        {
            if (IsCharacterEnterIn(collision.gameObject))
            {

            }
        }

        private bool IsCharacterEnterIn(GameObject player)
        {
            return player.TryGetComponent(out Transform component);
        }
    }
}