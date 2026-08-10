using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace World
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;
        [SerializeField] public TakeDamageEffect takeDamageEffect;
        [SerializeField] public List<InstantCharacterEffect> instantEffects;
    
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
}
