using System;
using System.Collections.Generic;
using Content.Scripts.Game.Player;
using UnityEngine;
using Zenject;

namespace Content.Scripts.Game.Level
{
    public class LevelLauncher : MonoBehaviour
    {
        public event Action OnPlayerFail;
        public event Action OnPlayerFinish;
        [SerializeField] private PlayerController player;
        [SerializeField] private ProgressHandler.ProgressHandler ProgressHandler;
        [SerializeField] private Transform startPoint;
        [SerializeField] private List<PlayerFailTrigger> FailTriggers = new List<PlayerFailTrigger>();
        [SerializeField] private LevelFinishTrigger LevelFinishTrigger;

        public void Init(Action OnInitialize = null)
        {
            ProgressHandler.StartPoint = startPoint;
            ProgressHandler.Init(startPoint.position.z);
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

            LevelFinishTrigger.OnPlayerWin += FinishGame;
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
            LevelFinishTrigger.OnPlayerWin -= FinishGame;
        }

        private void FinishGame()
        {
            OnPlayerFinish?.Invoke();
        }
    }
}
