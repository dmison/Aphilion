using UnityEngine;

namespace Character.AICharacters
{

    [CreateAssetMenu(menuName = "Aphelion/AI/States/Idle")]
    public class IdleState: AIState
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            // if we have target then: PURSUE
            if (aiCharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aiCharacter, aiCharacter.pursueState);
            }
            else
            {
                aiCharacter.animator.SetBool(IsMoving, false);
                aiCharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 0);
                // try to find a target
                aiCharacter.characterCombatManager.FindValidTarget();
            }

            return this;
        }
    }
}


