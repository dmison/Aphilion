using UnityEngine;

namespace Character.AICharacters
{

    [CreateAssetMenu(menuName = "Aphelion/AI/States/Idle")]
    public class IdleState: AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            // if we have target then: PURSUE
            if (aiCharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aiCharacter, aiCharacter.pursueState);
            }
            else
            {   
                // try to find a target
                aiCharacter.aiCharacterCombatManager.FindValidTarget();
            }

            return this;
        }
    }
}


