using UnityEngine;
using WeaponActions;

namespace Items
{
    public class WeaponItem : Item
    {
        [Header("Weapon Model")] 
        public GameObject weaponModel;

        [Header("Weapon Requirements")] 
        public int strengthREQ = 0;
        public int dexterityREQ = 0;
        
        // GUARD ABSORPTION (WHEN BLOCKING)
        
        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;
        public int fireDamage = 0;
        public int lightningDamage = 0;
        public int poisonDamage = 0;

        [Header("Attack Modifiers")] 
        public float lightAttack01_damage_mod = 1.1f;
        // HEAVY
        // LIGHT
        // CRITICAL

        [Header("Weapon Poise")]
        public int basePoiseDamage = 10;
        // OFFENSIVE BONUS - poise bonus granted by weapon if attacking
        
        [Header("Stamina Costs")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaModifier = 0.9f;
        // RUNNING
        // HEAVY
        // LIGHT
        
        // ITEM BASED ACTIONS (RT,RB,LT,LB)
        [Header("Actions")] 
        public WeaponItemAction OH_RB_Action; //One-handed, Right bumper action 

        // BLOCKING SOUNDS
    }
}