using System;
using Effects;
using UnityEngine;

namespace Character
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        CharacterManager character;
        // INSTANT EFFECTS (damage, healing)
        // OVER TIME EFFECTS (poison, healing over time)
        // STATIC EFFECTS (buffs, debuffs)
        
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProcessEffect(character);
        }
    
    }
}
