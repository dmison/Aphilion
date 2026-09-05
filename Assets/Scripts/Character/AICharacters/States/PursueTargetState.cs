using UnityEngine;

namespace Character.AICharacters.States
{
    [CreateAssetMenu(menuName = "Aphelion/AI/States/Pursue Target")]
    public class PursueTargetState : AIState
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

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
            
            
            // float angle = WorldUtilityManager.Instance.GetAngleToTarget(aiCharacter.transform, aiCharacter.characterCombatManager.DirectionToTarget);
            // if(angle is >= 25 or <= -25)
            // {
            //     return SwitchState(aiCharacter, aiCharacter.idleState);
            // }
            if (aiCharacter.characterCombatManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
            {
                return SwitchState(aiCharacter, aiCharacter.combatStanceState);
            }
            
            
            aiCharacter.animator.SetBool(IsMoving, true);
            aiCharacter.navMeshAgent.SetDestination(aiCharacter.characterCombatManager.currentLockTarget.transform.position);
            aiCharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 1);
            return this;    
        }
    }
}
