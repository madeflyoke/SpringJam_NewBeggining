using UnityEngine;

namespace Content.Scripts.Game.Player.Characters
{
    [CreateAssetMenu(fileName = "CharacterTeamConfig",
        menuName = "ScriptableObjects/Configs/New CharacterTeamConfig", order = 1)]
    public class CharacterTeamConfig : ScriptableObject
    {
        public float TeamUpDistance;
        public float DistanceBetweenChars;
        public float CharacterZOffset;
    }
}
