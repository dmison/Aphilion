using Character;
using Colliders;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class WeaponManager: MonoBehaviour
    {
        public MeleeWeaponDamageCollider damageCollider;
        private ParticleSystem _weaponTrail;
        
        private void Awake()
        {
            damageCollider = GetComponent<MeleeWeaponDamageCollider>();
            _weaponTrail = GetComponentInChildren<ParticleSystem>();
        }
        
        public void ToggleWeaponTrail(bool status)
        {
            if (!_weaponTrail) return;
            if (status)
            {
                _weaponTrail.Play();
            }
            else
            {
                _weaponTrail.Stop();
            }
            
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