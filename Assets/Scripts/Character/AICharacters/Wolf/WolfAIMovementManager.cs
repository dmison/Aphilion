using UnityEngine;

namespace Character.AICharacters.Wolf
{
    public class WolfAIMovementManager: CharacterMovementManager
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
        
        private WolfAICharacterManager _wolfManager;
        
        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        
        private Vector3 _rollDirection;
        private const float Gravity = 6;
        private float timeInAir = 0; 
        // ===========================================================================================

        protected override void Awake()
        {
            base.Awake();
            _wolfManager = GetComponent<WolfAICharacterManager>();
        }
        
        private void GroundCheck()
        {
            Vector3 position = transform.position;
            position.y += groundSphereRadius/2;
            _wolfManager.isGrounded = Physics.CheckSphere(position, groundSphereRadius, groundMask);
        }

        private void HandleFalling()
        {
            if (!_wolfManager.isGrounded)
            {
                timeInAir += Time.deltaTime;
                // FALLING
                _moveDirection = Vector3.zero;
                _moveDirection.y -= Gravity;
                _wolfManager.characterController.Move(_moveDirection * Time.deltaTime);
                
                // FALLING ANIMATION
                bool falling = timeInAir > 0.2f;
                _wolfManager.animator.SetBool("isGrounded", !falling);
                _wolfManager.animator.SetFloat("TimeInAir", timeInAir);
                _wolfManager.canMove = !falling;
                _wolfManager.canRotate = !falling;

            }
            else
            {   
                
                _wolfManager.animator.SetBool("isGrounded", true);
                _moveDirection.y = 0;
                timeInAir = 0;
                _wolfManager.animator.SetFloat("TimeInAir", timeInAir);
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