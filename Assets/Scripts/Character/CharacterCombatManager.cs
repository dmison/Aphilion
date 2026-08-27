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