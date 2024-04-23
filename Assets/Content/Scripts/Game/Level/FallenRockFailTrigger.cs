using UnityEngine;

namespace Content.Scripts.Game.Level
{
    public class FallenRockFailTrigger : PlayerFailTrigger
    {
        protected override bool ValidateCondition(Collider other)
        {
            return base.ValidateCondition(other) && transform.position.y> other.bounds.max.y;
        }
    }
}