using Items;
using UnityEngine;

namespace Character
{
    public class CharacterCombatManager: MonoBehaviour
    {
        protected CharacterManager _characterManager;
        public WeaponItem currentWeaponBeingUsed;
        public AttackTypes currentAttackType;
       
        [SerializeField] public float attackRotationSpeed = 25;
        
        public CharacterManager currentLockTarget;
        public bool lockedOn;
        public bool currentlyUsingRightHand;

        [Header("Target Info")]
        public float distanceFromTarget;
        public float viewableAngle;
        
        [SerializeField] public Transform lockOnTransform;
        
        public Vector3 DirectionToTarget => currentLockTarget ? (currentLockTarget.transform.position- lockOnTransform.position).normalized : Vector3.zero;
        public float AngleToTarget => currentLockTarget ? Vector3.Angle(transform.forward, DirectionToTarget) : 0;
        
        
        public virtual void RotateToAngle(float angle)
        {
        }

        protected virtual void Awake()
        {
            lockOnTransform = GetComponentInChildren<CharacterLockOnTarget>().transform;
            _characterManager = GetComponent<CharacterManager>();
        }

        public virtual void FindValidTarget()
        {
        }

        protected virtual void Update(){}
        

        public void RotateTowardsAgent(Transform agentTransform)
        {
            if (_characterManager.isMoving)
            {
                transform.rotation = agentTransform.rotation;
            }
        }

        public void RotateTowardsTargetWhilstAttacking(CharacterManager character)
        {
            if (currentLockTarget == null) return;
            if (!character.canRotate) return;
            if (!character.isPerformingAction) return;
            
            Vector3 targetDirection = currentLockTarget.transform.position - character.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            if(targetDirection == Vector3.zero) 
                targetDirection = character.transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            character.transform.rotation = Quaternion.Slerp(
                character.transform.rotation, 
                targetRotation, 
                Time.deltaTime * attackRotationSpeed);
        }
        
        public virtual void PivotTowardsTarget(CharacterManager character)
        {
            //lets do this without animation first
            if (!currentLockTarget) return;
            if (!_characterManager.canRotate) return;
            if (_characterManager.isPerformingAction) return;

            // hacked from the combat one below
            Vector3 targetDirection = currentLockTarget.transform.position - _characterManager.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            if(targetDirection == Vector3.zero) 
                targetDirection = _characterManager.transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            _characterManager.transform.rotation = Quaternion.Slerp(
                _characterManager.transform.rotation, 
                targetRotation, 
                Time.deltaTime * attackRotationSpeed);
        }
        
        public virtual void ReduceStaminaBasedOnAttack()
        {
            
        }
    }
}