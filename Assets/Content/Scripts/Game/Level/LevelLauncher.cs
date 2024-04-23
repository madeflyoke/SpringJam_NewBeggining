using System;
using Content.Scripts.Game.Player;
using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class LevelLauncher : MonoBehaviour
    {
        public Action OnPlayerFail;
        public event Action OnPlayerSpawn;
        public event Action OnPlayerFinish;
        [SerializeField] private PlayerController player;
        [SerializeField] private ProgressHandler.ProgressHandler ProgressHandler;
        [field: SerializeField] public Transform StartPoint { get; private set; }
        [SerializeField] private LevelFinishTrigger LevelFinishTrigger;

        public void Init(Action OnInitialize = null)
        {
            ProgressHandler.StartPoint = StartPoint;
            ProgressHandler.Init();
            player.Init();
            OnInitialize?.Invoke();
        }

        public void LaunchGameplay()
        {
            OnPlayerSpawn?.Invoke();
            ProgressHandler.isEnable = true;
            var spawnPos = ProgressHandler.LastCheckpoint == null
                ? StartPoint.position
                : ProgressHandler.LastCheckpoint.RespawnPoint;
            player.SpawnCharacter(spawnPos);
            player.Enable();
            
            LevelFinishTrigger.OnPlayerWin += FinishGame;
        }
        
        public void Disable()
        {
            ProgressHandler.isEnable = true;
            player.Disable();
            
            LevelFinishTrigger.OnPlayerWin -= FinishGame;
        }

        private void FinishGame()
        {
            OnPlayerFinish?.Invoke();
        }
    }
}
