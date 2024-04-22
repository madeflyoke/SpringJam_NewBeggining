using System;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class LevelFinishTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _requiredCharacter;
        [SerializeField] private CharacterType _type;
        
        public event Action OnPlayerWin;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Character otherChar))
            {
                if (otherChar.Type==_type)
                {
                    OnPlayerWin?.Invoke();
                    _requiredCharacter.gameObject.SetActive(false);
                }
                else
                {
                    _requiredCharacter.gameObject.SetActive(true);
                }
            }
        }
    }
}
