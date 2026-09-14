using UnityEngine;

namespace Character.Player
{
    public class PlayerSoundEffectsManager: CharacterSoundEffectsManager
    {
        [SerializeField] private AudioClip[] femaleScreams;
        [SerializeField] private AudioClip[] femaleHitGrunts;
        [SerializeField] private AudioClip[] maleScreams;
        [SerializeField] private AudioClip[] maleHitGrunts;
        private void PlayFemaleScream()
        {
            if (!(femaleScreams.Length > 0)) return;
            PlaySoundFX(WorldSoundFxManager.instance.ChooseRandomClip(femaleScreams));
        }

        private void PlayFemaleHitGrunt()
        {
            
            if (!(femaleHitGrunts.Length > 0)) return;
            PlaySoundFX(WorldSoundFxManager.instance.ChooseRandomClip(femaleHitGrunts));
        }

        public override void PlayHitGruntSoundFX()
        {
            if (_characterManager == null) return;
            if (_characterManager.IsDead) return;
            
            // select based on voice gender setting            
            PlayFemaleHitGrunt();
            // PlayMaleHitGrunt();
        }


        public override void PlayDie()
        {
            // select based on voice gender setting            
            PlayFemaleScream();
            // PlayMaleScream();
        } 
        
        
        public virtual void PlayAttack() { }
        
        public virtual void PlayIdle(){ }
    }
}