using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class CharacterSoundEffectsManager: MonoBehaviour
    {
        protected AudioSource _audioSource;

        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
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
        
    }
}