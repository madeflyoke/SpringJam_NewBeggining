using System;
using UnityEngine;

namespace SpringJam.Game.Character
{
    public class CharacterAnimationEventHandler : MonoBehaviour
    {
        public event Action Step;
        public event Action JumpStart;
        public event Action JumpEnd;

        public void StepHandler()
        {
            Step?.Invoke();
        }

        public void JumpStartHandler()
        {
            JumpStart?.Invoke();
        }

        public void JumpEndHandler()
        {
            JumpEnd?.Invoke();
        }
    }
}