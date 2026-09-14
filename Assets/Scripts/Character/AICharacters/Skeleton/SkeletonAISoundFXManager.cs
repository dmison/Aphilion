using UnityEngine;

namespace Character.AICharacters.Skeleton
{
    public class SkeletonAISoundFXManager: CharacterSoundEffectsManager
    {
        [SerializeField] private AudioClip[] aggro;
        [SerializeField] private AudioClip[] gruntHit;
        [SerializeField] private AudioClip[] gruntAttack;
        [SerializeField] private AudioClip[] die;
        
        private void PlayRandom(AudioClip[] clips)
        {
            PlaySoundFX(WorldSoundFxManager.instance.ChooseRandomClip(clips));
        }
        
        public override void PlayHitGruntSoundFX()
        {
            if(_characterManager && _characterManager.IsDead) return;
            PlayRandom(gruntHit);    
        }

        public override void PlayDie()
        {
            PlayRandom(die);
        }
        
        public override void PlayAggro()
        {
            PlayRandom(aggro);
        }

        public override void PlayAttack()
        {
            PlayRandom(gruntAttack);
        }
    }
}