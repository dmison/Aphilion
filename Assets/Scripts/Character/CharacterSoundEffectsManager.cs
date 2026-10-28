using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class CharacterSoundEffectsManager: MonoBehaviour
    {
        protected AudioSource _audioSource;
        protected CharacterManager _characterManager;
        

        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _characterManager = GetComponent<CharacterManager>();
        }

        public virtual void PlayRollSoundFX()
        {
            PlaySoundFX(WorldSoundFxManager.instance.rollSFX);
        }

        
        public virtual void PlaySoundFX(AudioClip soundFX, float volume = 1, bool randomPitch = true,
            float pitchRandom = 0.1f)
        {
            if (randomPitch)
            {
                _audioSource.pitch += Random.Range(-pitchRandom, pitchRandom);
            }
            _audioSource.PlayOneShot(soundFX, volume);
            _audioSource.pitch = 1;
        }
        protected void PlayRandom(AudioClip[] clips)
        {
            PlaySoundFX(WorldSoundFxManager.instance.ChooseRandomClip(clips));
        }
        public virtual void PlayHitGruntSoundFX()
        {
            
        }
        public virtual void PlayDie() { }
        public virtual void PlayDrown() { }
        public virtual void PlayAggro() { }
        public virtual void PlayAttack() { }
        
        public virtual void PlayIdle(){ }
    }
}


        
