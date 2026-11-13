using System.Collections;
using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private CharacterManager CurrentLockOnTarget;
    [SerializeField] private float _lockOnFollowSpeed = 0.2f;
    [SerializeField] private float _lockOnCameraHeight = 2.0f;
    [SerializeField] private float _unlockedCameraHeight = 1.8f;
    private Coroutine _setCameraHeight;
    [SerializeField] private float _cameraHeightSpeed = 0.9f;
    public static PlayerCameraManager instance;

    private bool _noPlayer = true;

    public void SetLockCameraHeight()
    {
        if (_setCameraHeight != null)
        {
            StopCoroutine(_setCameraHeight);
            _setCameraHeight = null;
        }
        _setCameraHeight = StartCoroutine(SetCameraHeight());
    }

    public void SetCameraLockOn(CharacterManager newTarget)
    {
        if (newTarget == null)
        {
            if (CurrentLockOnTarget != null) CurrentLockOnTarget.EnableOutline(false);
            CurrentLockOnTarget = null;
            return;
        }
        if (CurrentLockOnTarget != null && newTarget != CurrentLockOnTarget)
        {
            CurrentLockOnTarget.EnableOutline(false);
        }
        CurrentLockOnTarget = newTarget;
        CurrentLockOnTarget.EnableOutline(true);


    }

    private IEnumerator SetCameraHeight()
    {
        float duration = 1f;
        float timer = 0;

        Vector3 velocity = Vector3.zero;
        Vector3 newLockedCameraHeight = new Vector3(cameraPivot.localPosition.x, _lockOnCameraHeight, cameraPivot.localPosition.z);
        Vector3 newUnlockedCameraHeight = new Vector3(cameraPivot.localPosition.x, _unlockedCameraHeight, cameraPivot.localPosition.z);
        // while (timer < duration)
        // {
        //     timer += Time.deltaTime;
        //     if (playerManager.playerCombatManager.lockedOn)
        //     {
        //         cameraPivot.localPosition = Vector3.SmoothDamp(cameraPivot.localPosition, newLockedCameraHeight, ref velocity, _cameraHeightSpeed);
        //         cameraPivot.localRotation = Quaternion.Slerp(cameraPivot.localRotation, Quaternion.Euler(0,0,0), _lockOnFollowSpeed);
        //         
        //     }
        //     else
        //     {
        //         cameraPivot.localPosition = Vector3.SmoothDamp(cameraPivot.localPosition, newUnlockedCameraHeight, ref velocity, _cameraHeightSpeed);
        //         
        //     }
        //     Debug.Log("progress: "+cameraPivot.localPosition.y);
        //     yield return null;
        //     
        // }
        // Debug.Log("done: "+cameraPivot.localPosition.y);
        if (playerManager.characterCombatManager.lockedOn)
        {
            cameraPivot.localPosition = newLockedCameraHeight;
            cameraPivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            cameraPivot.localPosition = newUnlockedCameraHeight;
        }
        // Debug.Log("snapped: "+cameraPivot.localPosition.y);
        yield return null;

    }

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

    public void ResetPivotRotationToPlayer()
    {
        lookLeftRightAngle = 180;
        lookUpDownAngle = 0;
        // if (_noPlayer && playerManager)
        // {
        //     Debug.Log("pivot");
        //     _noPlayer = false;
        //     // Quaternion.RotateTowards(transform.rotation, );
        //     // cameraPivot.transform.rotation = playerManager.transform.rotation;
        // }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerManager = PlayerManager.Instance;
        CameraReset();
    }

    public void CameraReset()
    {
        _defaultCameraZPosition = playerCamera.transform.localPosition.z;
        lookLeftRightAngle = 180;
        lookUpDownAngle = 0;
        // ResetPivotRotationToPlayer();
    }

    public void HandleCameraActions()
    {
        if (!playerManager) return;
        // ResetPivotRotationToPlayer();
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
        if (CurrentLockOnTarget)
        {
            // rotate left/right
            Vector3 rotationDirection = CurrentLockOnTarget.transform.position - transform.position;
            rotationDirection.Normalize();
            rotationDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lockOnFollowSpeed);

            //rotate pivot 
            rotationDirection = CurrentLockOnTarget.transform.position - cameraPivot.position;
            rotationDirection.Normalize();
            targetRotation = Quaternion.LookRotation(rotationDirection);
            cameraPivot.rotation = Quaternion.Slerp(cameraPivot.rotation, targetRotation, _lockOnFollowSpeed);

            // preserve values for unlock
            lookLeftRightAngle = transform.eulerAngles.y;
            lookUpDownAngle = transform.eulerAngles.x;
        }
        else
        {
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