using System;
using System.Collections.Generic;
using Content.Scripts.Game.Player;
using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class LevelLauncher : MonoBehaviour
    {
        public event Action OnPlayerFail;
        [SerializeField] private PlayerController player;
        [SerializeField] private ProgressHandler.ProgressHandler ProgressHandler;
        [SerializeField] private Transform startPoint;
        [SerializeField] private List<PlayerFailTrigger> FailTriggers = new List<PlayerFailTrigger>();

        public void Init(Action OnInitialize = null)
        {
            ProgressHandler.StartPoint = startPoint;
            ProgressHandler.Init();
            player.Init();
            OnInitialize?.Invoke();
        }

        public void LaunchGameplay()
        {
            ProgressHandler.isEnable = true;
            var spawnPos = ProgressHandler.LastCheckpoint == null
                ? startPoint.position
                : ProgressHandler.LastCheckpoint.RespawnPoint;
            player.SpawnCharacter(spawnPos);
            player.Enable();
            
            foreach (var playerFailTrigger in FailTriggers)
            {
                playerFailTrigger.OnPlayerFail += CatchPlayerFail;
            }
        }

        private void CatchPlayerFail() => OnPlayerFail?.Invoke();

        public void Disable()
        {
            ProgressHandler.isEnable = true;
            player.Disable();
            foreach (var playerFailTrigger in FailTriggers)
            {
                playerFailTrigger.OnPlayerFail -= CatchPlayerFail;
            }
        }
    }
}
