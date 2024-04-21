using System.Collections.Generic;
using Content.Scripts.Game.InputService;
using Content.Scripts.Game.Player.Characters;
using UnityEngine;
using Zenject;

namespace Content.Scripts.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character firstCharacter;
        [SerializeField] private Character secondCharacter;
        [Inject] private CharacterMotionConfig MotionConfig;
        [Inject] private InputHandler Input;
        [Inject] private CharacterTeamConfig TeamConfig;
        private Dictionary<CharacterType, Character> Characters;
        private CharacterType currentCharacter = CharacterType.DEFAULT;
        private bool isTeamUp;
        public bool isEnabled;
        public Vector3 startPosition;
        
        public void Init()
        {
            Characters = new Dictionary<CharacterType, Character>()
            {
                {CharacterType.Strongman, firstCharacter},
                {CharacterType.Trickster, secondCharacter},
            };
            UpdateCharacterStats();
            currentCharacter = CharacterType.Strongman;
            isTeamUp = true;
        }

        public void SpawnCharacter(Vector3 SpawnPosition)
        {
            startPosition = SpawnPosition;
            Characters[currentCharacter].MovementComponent.SetPosition(SpawnPosition);
            var secondCharPos = SpawnPosition + new Vector3(0, 0, TeamConfig.CharacterZOffset);
            Characters[GetOtherCharacterTypeType()].MovementComponent.SetPosition(secondCharPos);
        }

        private void SubscribeOnInputEvents()
        {
            Input.SubscribeOnInputEvent(KeysEventType.Jump, ApplyJump);
            Input.SubscribeOnInputEvent(KeysEventType.SelectFirst, ()=> SwitchCharacter(CharacterType.Strongman));
            Input.SubscribeOnInputEvent(KeysEventType.SelectSecond, ()=> SwitchCharacter(CharacterType.Trickster));
            Input.SubscribeOnInputEvent(KeysEventType.ChangeTeamStatus, ChangeTeamStatus);
        }

        private void RemoveInputEvents()
        {
            Input.UnsubscribeFromInputEvent(KeysEventType.Jump, ApplyJump);
            Input.SubscribeOnInputEvent(KeysEventType.ChangeTeamStatus, ChangeTeamStatus);
            Input.ClearEventHandlerOn(KeysEventType.SelectFirst);
            Input.ClearEventHandlerOn(KeysEventType.SelectSecond);
        }

        public void Enable()
        {
            isEnabled = true;
            SubscribeOnInputEvents();
            EnableCharacter(firstCharacter);
            EnableCharacter(secondCharacter);
            SwitchCharacter(currentCharacter);
            SetDistanceBetweenCharacters();
            Input.Enable();
        }
        
        public void Disable()
        {
            isEnabled = false;
            DisableCharacter(firstCharacter);
            DisableCharacter(secondCharacter);
            RemoveInputEvents();
            Input.Disable();
        }

        public void Update()
        {
            if (isEnabled)
                ApplyMoving();
        }

        private void ApplyMoving()
        {
            float horizontalDirection = Input.GetAxisRaw().x;
            if(horizontalDirection==0) return;
            
            if (isTeamUp)
            {
                firstCharacter.MovementComponent.Move(horizontalDirection);
                secondCharacter.MovementComponent.Move(horizontalDirection);
            }
            else
                Characters[currentCharacter].MovementComponent.Move(horizontalDirection);
        }

        private void ApplyJump()
        {
            if (isTeamUp)
            {
                firstCharacter.MovementComponent.Jump();
                secondCharacter.MovementComponent.Jump();
            }
            else
                Characters[currentCharacter].MovementComponent.Jump();
        }
        
        public void SwitchCharacter(CharacterType nextCharacter)
        {
            if (currentCharacter != nextCharacter)
            {
                if (!isTeamUp)
                {
                    DisableCharacter(Characters[currentCharacter]);
                    EnableCharacter(Characters[nextCharacter], true);
                }
                
                currentCharacter = nextCharacter;
                var otherCharacter = GetOtherCharacterTypeType();
                var posOfCurrent = Characters[currentCharacter].transform.position;
                var posOfOther = Characters[otherCharacter].transform.position;
                
                
                
                Characters[currentCharacter].MovementComponent.SetPosition(posOfCurrent);
                Characters[otherCharacter].MovementComponent.SetPosition(posOfOther);
            }
        }

        private void ChangeTeamStatus()
        {
            if (!isTeamUp)
            {
                var distBetween =
                    Vector3.Distance(firstCharacter.transform.position, secondCharacter.transform.position);
                if (distBetween <= TeamConfig.TeamUpDistance)
                {
                    SetDistanceBetweenCharacters();
                    isTeamUp = true;
                    EnableCharacter(firstCharacter);
                    EnableCharacter(secondCharacter);
                    UpdateCharacterStats();
                }
            }
            else
            {
                isTeamUp = !isTeamUp;
                DisableCharacter(firstCharacter);
                DisableCharacter(secondCharacter);
                EnableCharacter(Characters[currentCharacter], true);
                UpdateCharacterStats(true);
            }
        }

        private void UpdateCharacterStats(bool notDefault = false)
        {
            if (notDefault == false)
            {
                Characters[CharacterType.Strongman].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.DEFAULT));
                Characters[CharacterType.Trickster].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.DEFAULT));
            }
            else
            {
                Characters[CharacterType.Strongman].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.Strongman));
                Characters[CharacterType.Trickster].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.Trickster));
            }
        }

        private void SetDistanceBetweenCharacters()
        {
            var otherCharacter = GetOtherCharacterTypeType();

            var posOfCurrent = Characters[currentCharacter].transform.position;
            var posOfOther = Characters[otherCharacter].transform.position;

            float newXValue;
            if (posOfCurrent.x > posOfOther.x)
                newXValue = posOfCurrent.x - TeamConfig.DistanceBetweenChars;
            else
                newXValue = posOfCurrent.x + TeamConfig.DistanceBetweenChars;
            
            Characters[otherCharacter].MovementComponent.SetPosition(new Vector3(newXValue,posOfOther.y,posOfOther.z));
        }

        private CharacterType GetOtherCharacterTypeType()
        {
            CharacterType otherCharacter;
            if (currentCharacter == CharacterType.Strongman) otherCharacter = CharacterType.Trickster;
            else otherCharacter = CharacterType.Strongman;
            return otherCharacter;
        }

        private void EnableCharacter(Character character, bool interactorStatus = false)
        {
            character.MovementComponent.directionLock = false;
            character.MovementComponent.jumpLook = false;
            character.MovementComponent.isEnabled = true;
            character.SetSelected(true);
        }
        
        private void DisableCharacter(Character character)
        {
            character.MovementComponent.directionLock = true;
            character.MovementComponent.jumpLook = true;
            character.MovementComponent.isEnabled = false;
            character.SetSelected(false);
        }
        
        private void OnDrawGizmos()
        {
            if(currentCharacter==CharacterType.DEFAULT) return;
            Gizmos.color = Color.red;

            if (!isTeamUp)
            {
                var distBetween =
                    Vector3.Distance(firstCharacter.transform.position, secondCharacter.transform.position);
                if (distBetween <= TeamConfig.TeamUpDistance)
                {
                    var pos = Characters[currentCharacter].transform.position;
                    pos.y += 2;
                    Gizmos.DrawSphere(pos, 0.2f);
                }
            }
        }
    }
}
