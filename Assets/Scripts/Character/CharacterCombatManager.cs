using System;
using Items;
using UnityEngine;

namespace Character
{
    public class CharacterCombatManager: MonoBehaviour
    {
        public WeaponItem currentWeaponBeingUsed;
        public AttackTypes currentAttackType;
        [SerializeField] public Transform lockOnTransform;
        protected virtual void Awake()
        { 
        }
        
        protected virtual void Update(){}
    }
}