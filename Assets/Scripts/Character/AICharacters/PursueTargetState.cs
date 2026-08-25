using UnityEngine;

namespace Character.AICharacters
{
    [CreateAssetMenu(menuName = "Aphelion/AI/States/Pursue Target")]
    public class PursueTargetState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction) return this;

            if (!aiCharacter.aiCharacterCombatManager.currentLockTarget)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }

            if (!aiCharacter.navMeshAgent.enabled)
            {
                aiCharacter.navMeshAgent.enabled = true;
            }
            aiCharacter.navMeshAgent.SetDestination(aiCharacter.aiCharacterCombatManager.currentLockTarget.transform.position);
            
            return this;
        }
    }
}
