using Character;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu (fileName = "TakeDamageEffect", menuName = "Aphelion/Character Effects/Take Damage Effect")]
    public class TakeDamageEffect : InstantCharacterEffect
    {
        [Header("Damage")] 
        [SerializeField] public int physicalDamage = 0;
        [SerializeField] public int fireDamage = 0;
        [SerializeField] public int poisonDamage = 0;
        [SerializeField] public int lightningDamage = 0;
        [SerializeField] public int poiseDamage = 0;
        private bool _poiseIsBroken = false;
        private int _finalDamage = 0;
        
        [Header("Animation")]
        [SerializeField] public bool playAnimation = true;
        [SerializeField] public bool manuallySelectAnimation = false;
        [SerializeField] public string animationName;

        [Header("Sound FX")] 
        [SerializeField] public bool willPlayDamageSFX = true;
        [SerializeField] public AudioClip elementalDamageSoundFX;

        [Header("Impact direction")] 
        public float angleHitFrom;
        public Vector3 impactPoint;
        
        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);
            CalculateDamage(character);
        }

        private void CalculateDamage(CharacterManager character)
        {
            if(character==null) return;
            if (character.isDead) return;
            _finalDamage = physicalDamage + fireDamage + poisonDamage + lightningDamage;
            _finalDamage = (_finalDamage <= 0) ? 1 : _finalDamage;
            character.characterStatsManager.CurrentHealth -= _finalDamage;
        }
    }
}