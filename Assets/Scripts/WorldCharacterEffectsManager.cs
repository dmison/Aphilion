using System.Collections.Generic;
using Effects;
using UnityEngine;

public class WorldCharacterEffectsManager : MonoBehaviour
{
    static WorldCharacterEffectsManager instance;
    [SerializeField] private List<InstantCharacterEffect> instantEffects;
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
        GenerateEffectIds();
    }

    private void GenerateEffectIds()
    {
        for (int i = 0; i < instantEffects.Count; i++)
        {
            instantEffects[i].InstantEffectId = i;
        }
    }
    
    // private void Start()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }
}
