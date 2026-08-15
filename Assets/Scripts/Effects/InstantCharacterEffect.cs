using Character;
using UnityEngine;

namespace Effects
{
    public class InstantCharacterEffect : ScriptableObject
    {
        public int InstantEffectId;

        public virtual void ProcessEffect(CharacterManager character)
        {
            
            
        }

        public virtual void PlayFX(CharacterManager character)
        {
        }
    }
}
