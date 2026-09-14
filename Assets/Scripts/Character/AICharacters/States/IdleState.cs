using UnityEngine;

namespace Character.AICharacters.States
{

    [CreateAssetMenu(menuName = "Aphelion/AI/States/Idle")]
    public class IdleState: AIState
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public override AIState Tick(AICharacterManager aICharacter)
        {
            // if we have a live target then: PURSUE
            if (aICharacter.characterCombatManager.currentLockTarget && 
                !aICharacter.characterCombatManager.currentLockTarget.IsDead
                )
            {
                // float angle = WorldUtilityManager.Instance.GetAngleToTarget(aiCharacter.transform, aiCharacter.characterCombatManager.DirectionToTarget);
                // if(angle is > 25 or < -25)
                // {
                //     aiCharacter.characterCombatManager.RotateToAngle(angle);
                // }
                // else
                // {
                
                aICharacter.characterSoundEffectsManager.PlayAggro();
                    return SwitchState(aICharacter, aICharacter.pursueState);
                // }
            }
            else
            {
                aICharacter.animator.SetBool(IsMoving, false);
                aICharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 0);
                // try to find a target
                aICharacter.characterCombatManager.FindValidTarget();
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
