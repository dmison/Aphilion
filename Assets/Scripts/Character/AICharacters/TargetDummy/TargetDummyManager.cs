namespace Character.AICharacters.TargetDummy
{
    public class TargetDummyManager : CharacterManager
    {
        protected override void Awake()
        {
            base.Awake();
            characterEffectsManager = GetComponent<TargetDummyEffectsManager>();

        }
    }
}
