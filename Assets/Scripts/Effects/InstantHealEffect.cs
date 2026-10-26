using System.Collections;
using Character;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu (fileName = "InstantHealEffect", menuName = "Aphelion/Character Effects/Instant/Heal Effect")]
    public class InstantHealEffect : InstantCharacterEffect
    {
        [SerializeField] private int healingAmount = 30;
        
        // [Header("Animation")]
        [SerializeField] public string animationName;
        [SerializeField] public ParticleSystem particles;
        //
        [Header("Sound FX")] 
        [SerializeField] public AudioClip soundFX;
        //
        // [Header("Impact direction")] 
        // public float angleHitFrom;
        // public Vector3 impactPoint;

        
        public override bool ProcessEffect(CharacterManager character)
        {
            // base.ProcessEffect(character);
            if (character.IsDead) return false;
            // CalculateDamage(character);
            // PlayFX(character);
            Debug.Log("CurrentHealth: "+character.characterStatsManager.CurrentHealth);
            Debug.Log("MaxHealth: "+character.characterStatsManager.MaxHealth);
            if (character.characterStatsManager.CurrentHealth >= character.characterStatsManager.MaxHealth)
                return false;
            
            if (animationName.Length > 0)
            {
                character.characterAnimationManager.PlayTargetActionAnimation(
                    "SoulHarvest", 
                    true, 
                    false,
                    true, 
                    true
                );
            }
            
            if(particles)
            {
                Instantiate(particles, character.transform);
                //(particles, character.transform.position, Quaternion.identity);
            }

            
            if (soundFX)
            {
                character.characterSoundEffectsManager.PlaySoundFX(soundFX);
            }
            character.characterStatsManager.CurrentHealth += healingAmount;
            Debug.Log("doing a heal");
            // PlayDirectionalDamageAnimation(character);
            return true;

        }
        //
        // private void CalculateDamage(CharacterManager character)
        // {
        //     if(character==null) return;
        //     
        //     _finalDamage = physicalDamage + fireDamage + poisonDamage + lightningDamage;
        //     _finalDamage = (_finalDamage <= 0) ? 1 : _finalDamage;
        //
        //     character.characterStatsManager.CurrentHealth -= _finalDamage;
        //     
        // }
        //
        // public override void PlayFX(CharacterManager character)
        // {
        //     character.characterEffectsManager.PlayBloodSpatterVFX(impactPoint);
        //     AudioClip clip = WorldSoundFxManager.instance.ChooseRandomClip(WorldSoundFxManager.instance.genericHitSFX);
        //
        //     Debug.Log(character);
        //     
        //     character.characterSoundEffectsManager.PlaySoundFX(clip);
        //     character.characterSoundEffectsManager.PlayHitGruntSoundFX();
        // }
        //
        // private void PlayDirectionalDamageAnimation(CharacterManager character)
        // {
        //     // _poiseIsBroken = true; //temp for testing
        //
        //     if (angleHitFrom is >= 145 and <= 180)
        //     {
        //         // FRONT
        //         character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_F_React_Sword, false);
        //     }
        //     else if (angleHitFrom is <= -145 and >= -180)
        //     {
        //         // FRONT
        //         character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_F_React_Sword, false);
        //     } 
        //     else if (angleHitFrom is >= -45 and <= 45)
        //     {
        //         // BACK
        //         character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_B_React_Sword, false);
        //     }
        //     else if (angleHitFrom is >= -144 and <= -45)
        //     {
        //         // LEFT
        //         character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_L_React_Sword, true);
        //     } 
        //     else if (angleHitFrom is >= 45 and <= 144)
        //     {
        //         //right
        //         character.characterAnimationManager.PlayTargetActionAnimation(character.characterAnimationManager.A_Hit_R_React_Sword, true);
        //     }
        //     
        // }
    }
}