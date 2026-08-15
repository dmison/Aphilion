using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldSoundFxManager : MonoBehaviour
{
    public static WorldSoundFxManager instance;

    [Header("Action Sounds")]
    public AudioClip rollSFX; 
    
    [Header("Hit Sounds")]
    public AudioClip[] genericHitSFX;
    

    public AudioClip ChooseRandomClip(AudioClip[] clips)
    {
        int i = Random.Range(0, clips.Length);
        return clips[i];
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
