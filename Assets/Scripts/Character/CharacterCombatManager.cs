using Items;
using UnityEngine;

namespace Character
{
    public class CharacterCombatManager: MonoBehaviour
    {
        private CharacterManager _characterManager;
        public WeaponItem currentWeaponBeingUsed;
        public AttackTypes currentAttackType;
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
        
        public virtual void PivotTowardsTarget(CharacterManager character)
        {
            // TODO: implement rotation
            Debug.Log("base method");
        }

        public void RotateTowardsAgent(Transform agentTransform)
        {
            if (_characterManager.isMoving)
            {
                transform.rotation = agentTransform.rotation;
            }
        }

    }
}