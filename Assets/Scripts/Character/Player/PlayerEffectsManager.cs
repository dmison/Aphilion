using Effects;

namespace Character.Player
{
    public interface IEffectsManager
    {
        public void ProcessInstantEffect(InstantCharacterEffect effect);
    }

    public class PlayerEffectsManager : CharacterEffectsManager, IEffectsManager
    {
        private PlayerManager player; 
        protected override void Awake()
        {
            // base.Awake();
            player = GetComponent<PlayerManager>();
        }
        public override void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            // base.ProcessInstantEffect(effect);
            effect.ProcessEffect(player);
        
        }
    }
}