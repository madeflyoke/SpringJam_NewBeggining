using System;
using DG.Tweening;
using UnityEngine;

namespace Player.Characters
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
        
        private CharacterMotionData MotionData;
        public LookDirection LookDirection { get; private set; }
        public bool IsGrounded { get; private set; }

        public bool directionLock;
        public bool jumpLook;
        public bool isEnabled;
        private Vector3 velocity;
        
        public void Start()
        {
            LookDirection = LookDirection.Right;
        }

        public void UpdateMotionData(CharacterMotionData data)
            => MotionData = data;
           

        private void UpdateLookDirection(float direction)
        {
            var newDir = LookDirection;
            float yRotation = 0;
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

        public void Move(float direction)
        {
            if (!isEnabled) return;

            if (!directionLock)
                UpdateLookDirection(direction);
            HorizontalMove(direction);
        }

        public void SetPosition(Vector3 position)
        {
            controller.Move(Vector3.down * Time.deltaTime * MotionData.Speed);
            controller.enabled = false;
            transform.position = position;
            controller.enabled = true;
        }

        private void Update()
        {
            IsGrounded = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, groundMask);
            if (IsGrounded && velocity.y < 0)
                velocity.y = -2f;

            velocity.y += GRAVITY * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        private void HorizontalMove(float direction)
        {
            var motionDir = new Vector3(direction, 0, 0);
            controller.Move(motionDir * (MotionData.Speed * Time.deltaTime));
        }

        public void Jump()
        {
            if (!isEnabled) return;
            if (jumpLook) return;

            if (IsGrounded)
            {
                velocity.y = Mathf.Sqrt(MotionData.JumpHeigh * -2 * GRAVITY);
                controller.Move(velocity * Time.deltaTime);
            }
        }

        private void OnDrawGizmos()
        {
            if (groundChecker == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundChecker.position, groundCheckerRadius);
        }
    }

    public enum LookDirection
    {
        Left,
        Right
    }
}