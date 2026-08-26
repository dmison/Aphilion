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
        
        [SerializeField] public Transform lockOnTransform;
        
        
        protected virtual void Awake()
        {
            lockOnTransform = GetComponentInChildren<CharacterLockOnTarget>().transform;
            
        }
        
        protected virtual void Update(){}
    }
}