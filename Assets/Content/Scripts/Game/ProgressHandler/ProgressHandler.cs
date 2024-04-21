using System.Collections.Generic;
using UnityEngine;

namespace Content.Scripts.Game.ProgressHandler
{
    public class ProgressHandler : MonoBehaviour
    {
      
        [SerializeField] private List<Checkpoint> Checkpoints= new List<Checkpoint>();
        public Transform StartPoint;
        public Checkpoint LastCheckpoint { get; private set; }
        public bool isEnable;

        public void Init()
        {
            LastCheckpoint = null;
            foreach (var checkpoint in Checkpoints)
            {
                checkpoint.OnPlayerReach += UpdateLastCheckpoint;
            }
        }

        private void UpdateLastCheckpoint(Checkpoint reachedPoint)
        {
            if(isEnable==false) return;
            if (LastCheckpoint == null)
                LastCheckpoint = reachedPoint;
            else
            {
                var distFromLast = Vector3.Distance(StartPoint.position, LastCheckpoint.RespawnPoint);
                var distFromReached = Vector3.Distance(StartPoint.position, reachedPoint.RespawnPoint);

                if (distFromReached > distFromLast)
                    LastCheckpoint = reachedPoint;
            }
        }
    }
}
