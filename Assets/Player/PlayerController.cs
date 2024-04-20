using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character firstCharacter;
        [SerializeField] private Character secondCharacter;
        [SerializeField] private float teamUpDistance;
        private Character currentCharacter;
        private bool isTeamUp;
        private Vector2 axis;

        public void SwitchCharacter(Character nextchar)
        {
            if (currentCharacter != nextchar)
            {
                if (!isTeamUp)
                {
                    DisableCharacter(currentCharacter);
                    EnableCharacter(nextchar, true);
                }
                
                currentCharacter = nextchar;
            }
        }

        public void Start()
        {
            isTeamUp = true;
            EnableCharacter(firstCharacter);
            EnableCharacter(secondCharacter);
            SwitchCharacter(firstCharacter);
        }

        public void Update()
        {
            //DEBUG INPUT
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchCharacter(firstCharacter);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchCharacter(secondCharacter);
            if (Input.GetKeyDown(KeyCode.R))
                ChangeTeamStatus();

            axis.x = Input.GetAxisRaw("Horizontal");
            axis.y = Input.GetAxisRaw("Vertical");
            if(axis!=Vector2.zero)
                ApplyMoving();
            if (Input.GetKeyDown(KeyCode.Space))
                ApplyJump();
        }

        private void ApplyMoving()
        {
            if (isTeamUp)
            {
                firstCharacter.MovementComponent.Move(axis);
                secondCharacter.MovementComponent.Move(axis);
            }
            else
                currentCharacter.MovementComponent.Move(axis);
        }

        private void ApplyJump()
        {
            if (isTeamUp)
            {
                firstCharacter.MovementComponent.Jump();
                secondCharacter.MovementComponent.Jump();
            }
            else
                currentCharacter.MovementComponent.Jump();
        }

        private void ChangeTeamStatus()
        {
            if (!isTeamUp)
            {
                var distBetween =
                    Vector3.Distance(firstCharacter.transform.position, secondCharacter.transform.position);
                if (distBetween <= teamUpDistance)
                {
                    isTeamUp = true;
                    EnableCharacter(firstCharacter);
                    EnableCharacter(secondCharacter);
                }
            }
            else
            {
                isTeamUp = !isTeamUp;
                DisableCharacter(firstCharacter);
                DisableCharacter(secondCharacter);
                EnableCharacter(currentCharacter, true);
            }
        }

        private void OnDrawGizmos()
        {
            if(currentCharacter==null) return;
            Gizmos.color = Color.red;

            if (!isTeamUp)
            {
                var distBetween =
                    Vector3.Distance(firstCharacter.transform.position, secondCharacter.transform.position);
                if (distBetween <= teamUpDistance)
                {
                    var pos = currentCharacter.transform.position;
                    pos.y += 2;
                    Gizmos.DrawSphere(pos, 0.2f);
                }
            }
        }

        private void EnableCharacter(Character character, bool interactorStatus = false)
        {
            character.MovementComponent.directionLock = false;
            character.MovementComponent.jumpLook = false;
            character.MovementComponent.isEnabled = true;
        }
        
        private void DisableCharacter(Character character)
        {
            character.MovementComponent.directionLock = true;
            character.MovementComponent.jumpLook = true;
            character.MovementComponent.isEnabled = false;
        }
    }
}
