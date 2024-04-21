using System;
using System.Collections.Generic;
using UnityEngine;

namespace Content.Scripts.Game.Player.Characters
{
    [CreateAssetMenu(fileName = "CharacterMotionConfig",
        menuName = "ScriptableObjects/Configs/New CharacterMotionConfig", order = 1)]
    public class CharacterMotionConfig : ScriptableObject
    {
        public List<CharacterMotionData> MotionDatas = new List<CharacterMotionData>();

        public CharacterMotionData GetData(CharacterType Type)
        {
            var data = MotionDatas.Find(d => d.Type == Type);
            if (data == null)
                data = MotionDatas.Find(d => d.Type == CharacterType.DEFAULT);
            return data;
        }
    }

    [Serializable]
    public class CharacterMotionData
    {
        public CharacterType Type;
        public float Speed;
        public float JumpHeigh;
    }
}