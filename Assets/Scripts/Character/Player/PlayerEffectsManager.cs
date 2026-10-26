using Effects;

namespace Character.Player
{
    public interface IEffectsManager
    {
        public bool ProcessInstantEffect(InstantCharacterEffect effect);
    }

    public class PlayerEffectsManager : CharacterEffectsManager, IEffectsManager
    {
        private PlayerManager player; 
        protected override void Awake()
        {
            // base.Awake();
            player = GetComponent<PlayerManager>();
        }
        public override bool ProcessInstantEffect(InstantCharacterEffect effect)
        {
            // base.ProcessInstantEffect(effect);
            return effect.ProcessEffect(player);
         
        }
    }
}