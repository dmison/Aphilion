using System;
using Effects;
using UnityEngine;
using World;

namespace Character
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        
        // INSTANT EFFECTS (damage, healing)
        // OVER TIME EFFECTS (poison, healing over time)
        // STATIC EFFECTS (buffs, debuffs)
        protected CharacterManager character;
        
        [Header("VFX")]
        [SerializeField] GameObject bloodSpatterVFX;
        
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProcessEffect(character);
        }

        public void PlayBloodSpatterVFX(Vector3 position)
        {
            if (bloodSpatterVFX != null)
            {
                Instantiate(bloodSpatterVFX, position, Quaternion.identity);
            }
            else
            {
                GameObject blood = Instantiate(WorldCharacterEffectsManager.instance.bloodSpatterVFX, position, Quaternion.identity);
            }
        }
        
    }
}
