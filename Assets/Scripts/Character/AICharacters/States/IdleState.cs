using UnityEngine;

namespace Character.AICharacters.States
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
                // float angle = WorldUtilityManager.Instance.GetAngleToTarget(aiCharacter.transform, aiCharacter.characterCombatManager.DirectionToTarget);
                // if(angle is > 25 or < -25)
                // {
                //     aiCharacter.characterCombatManager.RotateToAngle(angle);
                // }
                // else
                // {
                    return SwitchState(aiCharacter, aiCharacter.pursueState);
                // }
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

//
// if (characterCombatManager.currentLockTarget)
// {
//                 
//     characterCombatManager.RotateToAngle(angle);
//                 
//     if(angle is >= 25 or <= -25)
//     {
//         animator.SetBool("IsMoving", false);
//     }
// }
