using System;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class CharacterMovementComponent : MonoBehaviour
    {
        private const float GRAVITY = -9.81f;
        public event Action<LookDirection> LookDirectionChanged;
        public event Action<LookDirection> MoveDirectionChanged;
        
        [SerializeField] private Transform groundChecker;
        [SerializeField] private CharacterController controller;
        [SerializeField] private float groundCheckerRadius;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform ModelContainer;
        public Vector3 ControllerVelocity => controller.velocity;
        
        public float speed;
        public float jumpHeight;
        public LookDirection LookDirection { get; private set; }
        public MoveDirection MoveDirection { get; private set; }
        public bool directionLock;
        public bool jumpLook;
        public bool isEnabled;
        private bool isGrounded;
        private Vector3 velocity;
        
        public void Start()
        {
            LookDirection = LookDirection.Right;
            MoveDirection = MoveDirection.Horizontal;
        }

        public void UpdateMotionData(float newSpeed, float newJumpHeight)
        {
            speed = newSpeed;
            jumpHeight = newJumpHeight;
        }

        private void UpdateLookDirection(float direction)
        {
            var newDir = LookDirection;
            float yRotation=0;
            if (direction > 0)
            {
                newDir = LookDirection.Right;
                yRotation = 0;
            }
             
            if (direction < 0)
            {
                newDir = LookDirection.Left;
                yRotation = 180;
            }
              

            if (newDir != LookDirection)
            {
                LookDirection = newDir;
                ModelContainer.DOKill();
                ModelContainer.DORotate(new Vector3(0, yRotation, 0), 0.2f);
                LookDirectionChanged?.Invoke(LookDirection);
            }
        }
        public void Move(Vector2 direction)
        {
            if(!isEnabled) return;
            
            if (!directionLock)
                UpdateLookDirection(direction.x);
            
            switch (MoveDirection)
            {
                case MoveDirection.Horizontal:
                    HorizontalMove(direction);
                    break;
                case MoveDirection.Vertical:
                    VerticalMove(direction);
                    break;
            }
        }

        private void Update()
        {
            if (MoveDirection != MoveDirection.Vertical)
            {
                isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, groundMask);
                if (isGrounded && velocity.y < 0)
                    velocity.y = -2f;

                velocity.y += GRAVITY* Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
        }

        private void HorizontalMove(Vector2 direction)
        {
            direction.y = 0;
            controller.Move(direction * (speed * Time.deltaTime));
        }

        private void VerticalMove(Vector2 direction)
        {
            direction.x = 0;
            controller.Move(direction * (speed * Time.deltaTime));
        }

        public void Jump()
        {
            if(!isEnabled) return;
            if(jumpLook) return;

            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * GRAVITY);
                controller.Move(velocity *  Time.deltaTime);
            }
        }

        private void OnDrawGizmos()
        {
            if(groundChecker==null) return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundChecker.position, groundCheckerRadius);
        }
    }

    public enum LookDirection
    {
        Left,
        Right
    }
    
    public enum MoveDirection
    {
        Horizontal,
        Vertical
    }
}
