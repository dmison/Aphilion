using Character;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu (menuName = "Aphelion/Character Effects/Instant Effects/Take Stamina Damage Effect")]
    public class TakeStaminaDamageEffect: InstantCharacterEffect
    {
        [SerializeField] private float staminaDamage;
        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);
            character.characterStatsManager.CurrentStamina -= staminaDamage;
        }
    }
}