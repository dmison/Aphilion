using System;
using Effects;
using UnityEngine;

namespace Character
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        // INSTANT EFFECTS (damage, healing)
        // OVER TIME EFFECTS (poison, healing over time)
        // STATIC EFFECTS (buffs, debuffs)
        
        protected virtual void Awake()
        {
            
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            
        }
    
    }
}
