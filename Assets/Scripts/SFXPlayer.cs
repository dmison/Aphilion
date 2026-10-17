using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    protected void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundFX(AudioClip soundFX, float volume = 1, bool randomPitch = true,
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
