using Content.Scripts.Game.Player.Characters;
using UnityEngine;
using Zenject;

namespace Content.Scripts.Game.Level
{
    public class PlayerFailTrigger : MonoBehaviour
    {
        [Inject] private LevelLauncher _levelLauncher;
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Character>())
                TriggerEntered(other);
        }

        protected virtual void TriggerEntered(Collider other)
        {
            _levelLauncher.OnPlayerFail?.Invoke();
        }
    }
}
