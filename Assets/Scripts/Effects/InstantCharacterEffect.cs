using Character;
using UnityEngine;

namespace Effects
{
    public class InstantCharacterEffect : ScriptableObject
    {
        public int InstantEffectId;

        public virtual bool ProcessEffect(CharacterManager character)
        {
            return false;

        }
        

        public virtual void PlayFX(CharacterManager character)
        {
        }
    }
}
