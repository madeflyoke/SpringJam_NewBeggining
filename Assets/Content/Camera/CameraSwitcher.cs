using System;
using Cinemachine;
using Content.Scripts.Game.Player;
using Content.Scripts.Game.Player.Characters;
using EasyButtons;
using UnityEngine;

namespace Content.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private CinemachineVirtualCamera _vcam1;
        [SerializeField] private CinemachineVirtualCamera _vcam2;

        private void Awake()
        {
            _playerController.PlayerFocusedEvent += Switch;
        }

        private void OnDestroy()
        {
            _playerController.PlayerFocusedEvent -= Switch;
        }

        private void Switch(Character character)
        {
            var isVcam1 = _vcam1.Follow == character.transform;
            
            _vcam1.Priority = isVcam1? 11:10;
            _vcam2.Priority = isVcam1? 10:11;
        }

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            _playerController ??= FindObjectOfType<PlayerController>();
            var chars = FindObjectsOfType<Character>();
            _vcam1.Follow = chars[0].transform;
            _vcam1.Follow = chars[1].transform;
        }
        
#endif
       
    }
}
