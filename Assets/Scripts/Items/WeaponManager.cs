using Character;
using Colliders;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class WeaponManager: MonoBehaviour
    {
        public MeleeWeaponDamageCollider damageCollider;

        private void Awake()
        {
            damageCollider = GetComponent<MeleeWeaponDamageCollider>();
        }

        public void SetWeaponDamage(WeaponItem weapon, CharacterManager wielder)
        {
            damageCollider.characterWielding = wielder;
            damageCollider.physicalDamage = weapon.physicalDamage;
            damageCollider.fireDamage = weapon.fireDamage;
            damageCollider.poisonDamage = weapon.poisonDamage;
            damageCollider.lightningDamage = weapon.lightningDamage;
            
            damageCollider.Light_Attack_01_Modifier = weapon.lightAttack01_damage_mod;

        }
    }
}