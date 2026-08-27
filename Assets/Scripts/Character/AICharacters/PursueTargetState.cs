using UnityEngine;

namespace Character.AICharacters
{
    [CreateAssetMenu(menuName = "Aphelion/AI/States/Pursue Target")]
    public class PursueTargetState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction) return this;

            if (!aiCharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }

            if (!aiCharacter.navMeshAgent.enabled)
            {
                aiCharacter.navMeshAgent.enabled = true;
            }
            aiCharacter.animator.SetBool("IsMoving", true);
            aiCharacter.navMeshAgent.SetDestination(aiCharacter.characterCombatManager.currentLockTarget.transform.position);
            aiCharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 1);
            return this;
        }
    }
}
