using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.InputService
{
    [CreateAssetMenu(fileName = "New InputConfig", menuName = "ScriptableObjects/Configs/Create Input Handler Config", order = 1)]
    public class InputConfig : ScriptableObject
    {
        public List<ButtonPair> KeyboardButtonPairs = new List<ButtonPair>();

    }

    [Serializable]
    public struct ButtonPair
    {
        public InputInteractionType InteractionType;
        public KeysEventType EventType;
        public KeyCode KeyType;
    }
    
    public enum InputInteractionType
    {
        UP,
        DOWN
    }
}