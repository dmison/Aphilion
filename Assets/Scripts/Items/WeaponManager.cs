using Character;
using Colliders;
using UnityEngine;

namespace Items
{
    public class WeaponManager: MonoBehaviour
    {
        private MeleeWeaponDamageCollider _damageCollider;

        private void Awake()
        {
            _damageCollider = GetComponent<MeleeWeaponDamageCollider>();
        }

        public void SetWeaponDamage(WeaponItem weapon, CharacterManager wielder)
        {
            _damageCollider.characterWielding = wielder;
            _damageCollider.physicalDamage = weapon.physicalDamage;
            _damageCollider.fireDamage = weapon.fireDamage;
            _damageCollider.poisonDamage = weapon.poisonDamage;
            _damageCollider.lightningDamage = weapon.lightningDamage;
            
        }
    }
}