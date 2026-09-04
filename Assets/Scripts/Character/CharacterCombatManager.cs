using System;
using Items;
using UnityEngine;

namespace Character
{
    public class CharacterCombatManager: MonoBehaviour
    {
        public WeaponItem currentWeaponBeingUsed;
        public AttackTypes currentAttackType;
        public CharacterManager currentLockTarget;
        public bool lockedOn;
        public bool currentlyUsingRightHand;

        [SerializeField] public Transform lockOnTransform;
        
        public Vector3 DirectionToTarget => currentLockTarget ? (currentLockTarget.transform.position- lockOnTransform.position).normalized : Vector3.zero;
        public float AngleToTarget => currentLockTarget ? Vector3.Angle(transform.forward, DirectionToTarget) : 0;
        
        
        public virtual void RotateToAngle(float angle)
        {
        }

        protected virtual void Awake()
        {
            lockOnTransform = GetComponentInChildren<CharacterLockOnTarget>().transform;
            
        }

        public virtual void FindValidTarget()
        {
        }

        protected virtual void Update(){}
    }
}