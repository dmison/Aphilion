using Character.AI;
using UnityEngine;

namespace Character.Skeleton
{
    public class SkeletonMovementManager: CharacterMovementManager
    {
        [Header("Ground and Falling")]
        [SerializeField] private float groundSphereRadius = 0.15f;
        [SerializeField] private LayerMask groundMask;
        
        [Header("Speeds")]
        [SerializeField] private float walkSpeed = 1.5f;
        [SerializeField] private float runSpeed = 3.5f;
        [SerializeField] private float sprintSpeed = 6;
        [SerializeField] private float rotationSpeed = 15;
        
        [Header("Stamina Costs")]
        [SerializeField] private float sprintStaminaCostPerSecond = 20;
        [SerializeField] private float dodgeStaminaCost = 20;
        
        private AICharacterManager _skeletonManager;
        
        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        
        private Vector3 _rollDirection;
        private const float Gravity = 6;
        private float timeInAir = 0; 
        // ===========================================================================================

        protected override void Awake()
        {
            base.Awake();
            _skeletonManager = GetComponent<AICharacterManager>();
        }
        
        private void GroundCheck()
        {
            Vector3 position = transform.position;
            position.y += groundSphereRadius/2;
            _skeletonManager.isGrounded = Physics.CheckSphere(position, groundSphereRadius, groundMask);
        }

        private void HandleFalling()
        {
            if (!_skeletonManager.isGrounded)
            {
                timeInAir += Time.deltaTime;
                // FALLING
                _moveDirection = Vector3.zero;
                _moveDirection.y -= Gravity;
                _skeletonManager.characterController.Move(_moveDirection * Time.deltaTime);
                
                // FALLING ANIMATION
                bool falling = timeInAir > 0.2f;
                _skeletonManager.animator.SetBool("isGrounded", !falling);
                _skeletonManager.animator.SetFloat("TimeInAir", timeInAir);
                _skeletonManager.canMove = !falling;
                _skeletonManager.canRotate = !falling;

            }
            else
            {   
                
                _skeletonManager.animator.SetBool("isGrounded", true);
                _moveDirection.y = 0;
                timeInAir = 0;
                _skeletonManager.animator.SetFloat("TimeInAir", timeInAir);
            }
            
            
        }
        
        private void Update()
        {
            GroundCheck();
            HandleFalling();

            
        }
        private void OnDrawGizmos()
        {
            Vector3 position = transform.position;
            position.y += groundSphereRadius/2;
            Gizmos.DrawSphere(position, groundSphereRadius);
        }
    }
}