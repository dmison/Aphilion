using System.Collections.Generic;
using Character.Player;

namespace Character.AICharacters
{
    public class AICharacterCombatManager:  CharacterCombatManager
    {
        private AICharacterManager _aiCharacterManager;

        protected override void Awake()
        {
            base.Awake();
            _aiCharacterManager = GetComponent<AICharacterManager>();
            
        }
        
        
        public override void FindValidTarget()
        {
            List<CharacterManager> targets = _aiCharacterManager.senses.CharactersInVision();
            foreach (CharacterManager target in targets)
            {
                
                if (target.CompareTag("Player"))
                {
                    PlayerManager player = target.GetComponent<PlayerManager>();
                    currentLockTarget = player;
                    break;
                }
            }
        }
    }
}