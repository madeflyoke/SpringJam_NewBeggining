using System;
using System.Collections.Generic;
using Player.Characters;
using Player.InputService;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character firstCharacter;
        [SerializeField] private Character secondCharacter;
        [SerializeField] private float teamUpDistance;
        [SerializeField] private float distanceBetweenChars;
        [SerializeField] private float CharacterZOffset;
        [SerializeField] private CharacterMotionConfig MotionConfig;
        [SerializeField] private InputHandler Input;
        private Dictionary<CharacterType, Character> Characters;
        private CharacterType currentCharacter = CharacterType.DEFAULT;
        private bool isTeamUp;
        public bool isEnabled;
        

        public void Init()
        {
            Characters = new Dictionary<CharacterType, Character>()
            {
                {CharacterType.Strongman, firstCharacter},
                {CharacterType.Trickster, secondCharacter},
            };
            Characters[CharacterType.Strongman].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.DEFAULT));
            Characters[CharacterType.Trickster].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.DEFAULT));
            EnableCharacter(firstCharacter);
            EnableCharacter(secondCharacter);
            SwitchCharacter(CharacterType.Strongman);
            SetDistanceBetweenCharacters();
            SubscribeOnInputEvents();
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

        public void Start()
        {
            isTeamUp = true;
            Init();
            Input.Enable();
            isEnabled = true;
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
                posOfCurrent.z =CharacterZOffset;
                posOfOther.z=-CharacterZOffset;
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
                if (distBetween <= teamUpDistance)
                {
                    SetDistanceBetweenCharacters();
                    isTeamUp = true;
                    EnableCharacter(firstCharacter);
                    EnableCharacter(secondCharacter);
                    Characters[CharacterType.Strongman].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.DEFAULT));
                    Characters[CharacterType.Trickster].MovementComponent.UpdateMotionData(MotionConfig.GetData(CharacterType.DEFAULT));
                }
            }
            else
            {
                isTeamUp = !isTeamUp;
                DisableCharacter(firstCharacter);
                DisableCharacter(secondCharacter);
                EnableCharacter(Characters[currentCharacter], true);
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
                newXValue = posOfCurrent.x - distanceBetweenChars;
            else
                newXValue = posOfCurrent.x + distanceBetweenChars;
            
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
            character.Interactor.isEnabled = interactorStatus;
            character.MovementComponent.directionLock = false;
            character.MovementComponent.jumpLook = false;
            character.MovementComponent.isEnabled = true;
        }
        
        private void DisableCharacter(Character character)
        {
            character.Interactor.isEnabled = false;
            character.MovementComponent.directionLock = true;
            character.MovementComponent.jumpLook = true;
            character.MovementComponent.isEnabled = false;
        }
        
        private void OnDrawGizmos()
        {
            if(currentCharacter==CharacterType.DEFAULT) return;
            Gizmos.color = Color.red;

            if (!isTeamUp)
            {
                var distBetween =
                    Vector3.Distance(firstCharacter.transform.position, secondCharacter.transform.position);
                if (distBetween <= teamUpDistance)
                {
                    var pos = Characters[currentCharacter].transform.position;
                    pos.y += 2;
                    Gizmos.DrawSphere(pos, 0.2f);
                }
            }
        }
    }
}
