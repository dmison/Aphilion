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
            PlayFX(character);
            if (character.isDead) return;
            PlayDirectionalDamageAnimation(character);
            
        }

        private void CalculateDamage(CharacterManager character)
        {
            if(character==null) return;
            
            _finalDamage = physicalDamage + fireDamage + poisonDamage + lightningDamage;
            _finalDamage = (_finalDamage <= 0) ? 1 : _finalDamage;
            character.characterStatsManager.CurrentHealth -= _finalDamage;
            
        }

        public override void PlayFX(CharacterManager character)
        {
            character.characterEffectsManager.PlayBloodSpatterVFX(impactPoint);
            AudioClip clip = WorldSoundFxManager.instance.ChooseRandomClip(WorldSoundFxManager.instance.genericHitSFX);

            
            
            character.characterSoundEffectsManager.PlaySoundFX(clip);
            
        }

        private void PlayDirectionalDamageAnimation(CharacterManager character)
        {
            // _poiseIsBroken = true; //temp for testing

            if (angleHitFrom is >= 145 and <= 180)
            {
                // FRONT
                character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_F_React_Sword, false);
            }
            else if (angleHitFrom is <= -145 and >= -180)
            {
                // FRONT
                character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_F_React_Sword, false);
            } 
            else if (angleHitFrom is >= -45 and <= 45)
            {
                // BACK
                character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_B_React_Sword, false);
            }
            else if (angleHitFrom is >= -144 and <= -45)
            {
                // LEFT
                character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_L_React_Sword, true);
            } 
            else if (angleHitFrom is >= 45 and <= 144)
            {
                //right
                character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_R_React_Sword, true);
            }
            
        }
    }
}