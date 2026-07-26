using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerCameraManager : MonoBehaviour
    {
        [SerializeField] private float lookUpDownAngle;
        [SerializeField] private float lookLeftRightAngle;
        [SerializeField] private float horizontalLookSensitivity = 220;
        [SerializeField] private float verticalLookSensitivity = 220;
        [SerializeField] private float minimumLookAngle = -30;
        [SerializeField] private float maximumLookAngle = 60;

        [SerializeField] private float cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask collisionMask;
        
        private float _targetCameraZPosition;
        private float _defaultCameraZPosition;
        private Vector3 _cameraObjectPosition;
        
        private Vector3 _cameraVelocity;
        private float _cameraSmoothingSpeed;
        
        
        public PlayerManager playerManager;
        [SerializeField] public Camera playerCamera;
        [SerializeField] public Transform cameraPivot;
        private InputAction _lookAction;
        private Vector2 _cameraInput;

        
        
        public static PlayerCameraManager instance;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _lookAction = InputSystem.actions.FindAction("Look");
            _lookAction.performed += ctx => _cameraInput = ctx.ReadValue<Vector2>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _defaultCameraZPosition = playerCamera.transform.localPosition.z;
        }

        public void HandleCameraActions()
        {
            if (!playerManager) return;
            HandleFollowPlayer();
            HandleRotations();
            HandleCollisions();
        }

        private void HandleCollisions()
        {
            _targetCameraZPosition = _defaultCameraZPosition;

            Vector3 direction = playerCamera.transform.position - cameraPivot.position;
            direction.Normalize();
            if (Physics.SphereCast(
                    cameraPivot.position, 
                    cameraCollisionRadius, 
                    direction, 
                    out RaycastHit hit, 
                    Mathf.Abs(_targetCameraZPosition), 
                    collisionMask))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivot.position, hit.point);
                _targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }
        
            if (Mathf.Abs(_targetCameraZPosition) < cameraCollisionRadius)
            {
                _targetCameraZPosition = -_defaultCameraZPosition;
            }
            _cameraObjectPosition.z = Mathf.Lerp(playerCamera.transform.localPosition.z, _targetCameraZPosition, 0.2f);
            playerCamera.transform.localPosition = _cameraObjectPosition;
            
        }
        
        private void HandleFollowPlayer()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(
                transform.position, 
                playerManager.transform.position, 
                ref _cameraVelocity, 
                _cameraSmoothingSpeed * Time.deltaTime
                );
            transform.position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            // if locked on 
            // force rotation to target
            // else 

            // rotate left/right
            lookLeftRightAngle += _cameraInput.x * horizontalLookSensitivity * Time.deltaTime;
            Vector3 cameraRotation = Vector3.zero;
            cameraRotation.y = lookLeftRightAngle;
            Quaternion lrTargetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = lrTargetRotation;

            //rotate pivot up/down
            Vector3 pivotRotation = Vector3.zero;
            lookUpDownAngle -= _cameraInput.y * verticalLookSensitivity * Time.deltaTime;
            lookUpDownAngle = Mathf.Clamp(lookUpDownAngle, minimumLookAngle, maximumLookAngle);
            pivotRotation = Vector3.zero;
            pivotRotation.x = lookUpDownAngle;
            Quaternion udTargetRotation = Quaternion.Euler(pivotRotation);
            cameraPivot.localRotation = udTargetRotation;
            
        }
    }
}
