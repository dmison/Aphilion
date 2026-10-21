using Character.AICharacters.States;
using UnityEngine;

namespace Character.AICharacters.Wolf.States
{
    [CreateAssetMenu(menuName = "Aphelion/AI/States/Wolf/Pursue Target")]
    public class WolfPursueTargetState : PursueTargetState
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public override AIState Tick(AICharacterManager aICharacter)
        {
            if (aICharacter.isPerformingAction) return this;

            if (!aICharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aICharacter, aICharacter.idleState);
            }

            if (!aICharacter.navMeshAgent.enabled)
            {
                aICharacter.navMeshAgent.enabled = true;
            }
            
            
            // float angle = WorldUtilityManager.Instance.GetAngleToTarget(aiCharacter.transform, aiCharacter.characterCombatManager.DirectionToTarget);
            // if(angle is >= 25 or <= -25)
            // {
            //     return SwitchState(aiCharacter, aiCharacter.idleState);
            // }
            if (aICharacter.characterCombatManager.distanceFromTarget <= aICharacter.navMeshAgent.stoppingDistance)
            {
                return SwitchState(aICharacter, aICharacter.combatStanceState);
            }
            
            
            // aICharacter.animator.SetBool(IsMoving, true);
            aICharacter.navMeshAgent.SetDestination(aICharacter.characterCombatManager.currentLockTarget.transform.position);
            aICharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 1);
            return this;    
        }
    }
}
