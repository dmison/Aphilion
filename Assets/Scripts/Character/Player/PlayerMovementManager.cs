using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerMovementManager: CharacterMovementManager
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
        
        
        private PlayerManager _playerManager;
        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        
        // INPUTS
        private InputAction _moveAction;
        [HideInInspector] public Vector2 moveInputVector = Vector2.zero;

        private InputAction _sprintAction;
        private bool _sprintInput = false;
        private bool _isSprinting = false;
        public bool IsSprinting => _isSprinting;
        
        private InputAction _dodgeAction;
        public bool _dodgeInput = false;
        private Vector3 _rollDirection;
        
        private const float Gravity = 6;
        
        private float timeInAir = 0; 
        // ===========================================================================================
        protected override void Awake()
        {
            base.Awake();
            _playerManager = GetComponent<PlayerManager>();
            
            _moveAction = InputSystem.actions.FindAction("Move");
            _moveAction.performed += MoveInput;
            _moveAction.canceled += MoveInputStop;
            
            _dodgeAction = InputSystem.actions.FindAction("Dodge");
            _dodgeAction.performed += DodgeInput;

            _sprintAction = InputSystem.actions.FindAction("Sprint");
            _sprintAction.performed += SprintInput;
            _sprintAction.canceled += SprintInputStop;
        }
        // ===========================================================================================
        private void MoveInput(InputAction.CallbackContext context)
        {
            moveInputVector = context.ReadValue<Vector2>();
        }
        private void MoveInputStop(InputAction.CallbackContext context)
        {
            moveInputVector = Vector2.zero;
        }
        private void DodgeInput(InputAction.CallbackContext context)
        {
            _dodgeInput = true;
        }
        private void SprintInput(InputAction.CallbackContext context)
        {
            HandleSprintInput(true);
        }
        private void SprintInputStop(InputAction.CallbackContext context)
        {
            HandleSprintInput(false);
        }
        // ===========================================================================================
        private void GroundCheck()
        {
            Vector3 position = transform.position;
            position.y += groundSphereRadius/2;
            _playerManager.isGrounded = Physics.CheckSphere(position, groundSphereRadius, groundMask);
        }

        private void HandleFalling()
        {
            if (!_playerManager.isGrounded)
            {
                timeInAir += Time.deltaTime;
                // FALLING
                _moveDirection = Vector3.zero;
                _moveDirection.y -= Gravity;
                _playerManager.characterController.Move(_moveDirection * Time.deltaTime);
                
                // FALLING ANIMATION
                bool falling = timeInAir > 0.2f;
                _playerManager.animator.SetBool("isGrounded", !falling);
                _playerManager.animator.SetFloat("TimeInAir", timeInAir);
                _playerManager.canMove = !falling;
                _playerManager.canRotate = !falling;

            }
            else
            {   
                
                _playerManager.animator.SetBool("isGrounded", true);
                _moveDirection.y = 0;
                timeInAir = 0;
                _playerManager.animator.SetFloat("TimeInAir", timeInAir);
            }
            
            
        }
        
        private void HandleSprintInput(bool sprintInput)
        {
            _isSprinting = _playerManager.isGrounded && 
                           !_playerManager.isPerformingAction && 
                           sprintInput && 
                           _playerManager.characterStatsManager.CurrentStamina > 0;
        }
        
        private float MoveAmount => _getMoveAmount(); 
        private float _getMoveAmount()
        {
            float move = Mathf.Clamp01( Mathf.Abs(moveInputVector.x) + Mathf.Abs(moveInputVector.y) );
            if (move > 0 && move <= 0.5) move = 0.5f;   // walk
            if (move > 0.5 && move <= 1)  move = 1.0f;  // run
            if(_isSprinting && move>0.5) move = 2.0f;   // sprint
            return move;
        }

        private float clamped (float x)
        {
            if (x > 0 && x <= 0.5) x = 0.5f; // walk
            if (x > 0.5 && x <= 1) x = 1.0f; // run
            if (_isSprinting && x > 0.5) x = 2.0f; // sprint
            return x;
        }
        
        private Vector2 _getClampedMoveVector()
        {
            return new Vector2(clamped(moveInputVector.x), clamped(moveInputVector.y));
        }
        
        private void HandleRotation()
        {
            if(!_playerManager.canRotate) return;
            if (_playerManager.IsDead) return;
            
            _targetRotationDirection = Vector3.zero;

            if (_playerManager.characterCombatManager.lockedOn && !_isSprinting)
            {
                if (!_playerManager.characterCombatManager.currentLockTarget) return;
                _targetRotationDirection = _playerManager.characterCombatManager.currentLockTarget.transform.position - transform.position;
                _targetRotationDirection.y = 0;
                _targetRotationDirection.Normalize();
                
            }
            else
            {
                _targetRotationDirection = 
                    (PlayerCameraManager.instance.playerCamera.transform.forward * moveInputVector.y) + 
                    (PlayerCameraManager.instance.playerCamera.transform.right * moveInputVector.x);
                _targetRotationDirection.Normalize();
                _targetRotationDirection.y = 0;
            
                if (_targetRotationDirection == Vector3.zero)
                {
                    _targetRotationDirection = transform.forward;
                }
                
            }
            Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = targetRotation;
            

        }
        
        private void HandleGroundedMovement()
        {
            if (!_playerManager.canMove) return;
            
            _moveDirection = 
                PlayerCameraManager.instance.transform.forward * moveInputVector.y + 
                PlayerCameraManager.instance.transform.right * moveInputVector.x;
            _moveDirection.Normalize();
            _moveDirection.y = 0;
            
            // if sprinting reduce stamina
            // if stamina then stop sprinting
            if (_isSprinting)
            {
                if (_playerManager.characterStatsManager.CurrentStamina > 0)
                {
                    _playerManager.characterStatsManager.CurrentStamina -= (sprintStaminaCostPerSecond * Time.deltaTime);
                }
                else
                {
                    _isSprinting = false;
                }
            }

            // set speed based on MoveAmount
            float speed = MoveAmount switch
            {
                0.5f => walkSpeed,
                1.0f => runSpeed,
                2.0f => sprintSpeed,
                _ => 0
            };

            _playerManager.animator.SetBool("IsMoving", speed != 0);
            
            _playerManager.characterController.Move(_moveDirection * (speed * Time.deltaTime));

            if (_playerManager.characterCombatManager.lockedOn && !_isSprinting)
            {
                Vector2 movement = _getClampedMoveVector();
                _playerManager.characterAnimationManager.UpdateAnimatorMovementParameters(movement.x, movement.y);
                
            }
            else
            {
                _playerManager.characterAnimationManager.UpdateAnimatorMovementParameters(0, MoveAmount);
            }
            
        }
        
        

        private void AttemptToDodge()
        {
            if (!_dodgeInput) return;
            _dodgeInput = false;
            
            if (!_playerManager.isGrounded) return;
            if (_playerManager.isPerformingAction) return;
            if (_playerManager.characterStatsManager.CurrentStamina <= 0) return;
            
            _playerManager.isPerformingAction = true;
            _playerManager.canMove = false;
            _playerManager.canRotate = false;
            
            if (MoveAmount > 0)
            {
                // DO ROLL
                _rollDirection = PlayerCameraManager.instance.playerCamera.transform.forward * moveInputVector.y;
                _rollDirection += PlayerCameraManager.instance.playerCamera.transform.right * moveInputVector.x;
                _rollDirection.y = 0;
                _rollDirection.Normalize();
                Quaternion playerRotation = Quaternion.LookRotation(_rollDirection);
                transform.rotation = playerRotation;
                _playerManager.characterAnimationManager.PlayTargetActionAnimation(
                    "Roll_RM_Forwards", 
                    _playerManager.isPerformingAction, 
                    true,
                    false, 
                    false
                    );
            }
            else
            {
                // do back step
                _playerManager.characterAnimationManager.PlayTargetActionAnimation(
                    "DodgeBack_RM",  
                    _playerManager.isPerformingAction, 
                    true,
                    false,
                    false
                    );
            }

            _playerManager.characterStatsManager.CurrentStamina -= dodgeStaminaCost;
        }
        
        private void Update()
        {
            if (_playerManager.IsActive)
            {
                GroundCheck();
                HandleFalling();
                HandleRotation();
                HandleGroundedMovement();
                AttemptToDodge();
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 position = transform.position;
            position.y += groundSphereRadius/2;
            Gizmos.DrawSphere(position, groundSphereRadius);
        }
    }
}