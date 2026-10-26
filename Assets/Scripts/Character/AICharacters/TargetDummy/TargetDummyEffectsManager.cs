using Effects;

namespace Character.AICharacters.TargetDummy
{
    public class TargetDummyEffectsManager: CharacterEffectsManager
    {
        public override bool ProcessInstantEffect(InstantCharacterEffect effect)
        {
            effect.PlayFX(character);
            character.characterAnimationManager.PlayTargetActionAnimation("BOING", true, false, false, false, 0);
            return true;
        }
    }
}