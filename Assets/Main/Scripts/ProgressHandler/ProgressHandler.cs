using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.ProgressHandler
{
    public class ProgressHandler : MonoBehaviour
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private List<Checkpoint> Checkpoints= new List<Checkpoint>();
        public Checkpoint LastCheckpoint { get; private set; }

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
