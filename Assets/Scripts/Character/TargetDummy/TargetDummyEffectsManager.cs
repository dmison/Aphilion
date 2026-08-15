using Effects;

namespace Character.TargetDummy
{
    public class TargetDummyEffectsManager: CharacterEffectsManager
    {
        public override void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            effect.PlayFX(character);
            character.characterAnimationManager.PlayTargetActionAnimation("BOING", true, false, false, false, 0);
        }
    }
}