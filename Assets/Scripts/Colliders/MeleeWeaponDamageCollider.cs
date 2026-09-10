using Character;
using Effects;
using UnityEngine;
using World;

namespace Colliders
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        public CharacterManager characterWielding;
        public float Light_Attack_01_Modifier;

        protected override void Awake()
        {
            base.Awake();
            damageCollider.enabled = false;
        }
        
        protected override void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            // get root player object
            CharacterManager character = other.gameObject.GetComponentInParent<CharacterManager>();
            if (character == null) return;
            if (character == characterWielding) return; //don't damage ourselves
            
            GameObject characterGO = character.gameObject;
            
            Debug.Log(characterGO.name);
         
            if (!LayerInLayerMask(character.gameObject.layer, canDamageLayers)) return;
            _contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        
         
            // if (character != null && characterGO.CompareTag("Player"))
            // {
            //    
                CalculateDamage(character);
            // }
            // TODO: Damageable interface???
        }
        
        protected override void CalculateDamage(CharacterManager character)
        {
            if (charactersDamaged.Contains(character)) return;
            charactersDamaged.Add(character);

            
            Debug.Log("deal damange to: "+ character.gameObject.name);
            int  damage = physicalDamage + fireDamage + poisonDamage + lightningDamage;
            TakeDamageEffect effect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            effect.physicalDamage = physicalDamage;
            effect.fireDamage = fireDamage;
            effect.poisonDamage = poisonDamage;
            effect.lightningDamage = lightningDamage;
            effect.poiseDamage = poiseDamage;
            effect.impactPoint = _contactPoint;
            effect.angleHitFrom = Vector3.SignedAngle(transform.forward, character.transform.forward, Vector3.up);
            float modifier = 1;
            
            modifier = characterWielding.characterCombatManager.currentAttackType switch
            {
                AttackTypes.LightAttack01 => Light_Attack_01_Modifier,
                _ => modifier
            };
            effect = ApplyDamageModifier(modifier, effect);
            
            character.characterEffectsManager.ProcessInstantEffect(effect);
        }

        private TakeDamageEffect ApplyDamageModifier(float modifier, TakeDamageEffect effect)
        {
            effect.physicalDamage = Mathf.RoundToInt(effect.physicalDamage * modifier);
            effect.fireDamage = Mathf.RoundToInt(effect.fireDamage * modifier);
            effect.poisonDamage = Mathf.RoundToInt(effect.poisonDamage * modifier);
            effect.lightningDamage = Mathf.RoundToInt(effect.lightningDamage * modifier);
            effect.poiseDamage  = Mathf.RoundToInt(effect.poiseDamage * modifier);
            return effect;
        }
    }
}
