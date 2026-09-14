using Character.AICharacters.Actions;
using UnityEngine;
namespace Character.AICharacters.States
{
    [CreateAssetMenu(menuName = "Aphelion/AI/States/Attack")]
    public class AttackState: AIState
    {
        [Header("Current Attack")]
        [HideInInspector] public AICharacterAttackAction currentAttack;
        [HideInInspector] public bool willPerformCombo = false;


        [Header("State Flags")] 
        protected bool hasPerformedAttack = false;
        protected bool hasPerformedCombo = false;
        
        [Header("Pivot After Attack")]
        [SerializeField] protected bool pivotAfterAttack = false;

        public override AIState Tick(AICharacterManager aICharacter)
        {
            if (!aICharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aICharacter, aICharacter.idleState);
            }
            
            if(aICharacter.characterCombatManager.currentLockTarget.IsDead)
            {
                return SwitchState(aICharacter, aICharacter.idleState);
            }
            // ROTATE TOWARDS CHARACTER WHILST ATTACKING
            aICharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 0);
            
            
            // PERFORM A COMBO
            if (willPerformCombo && !hasPerformedCombo)
            {
                if (currentAttack.comboAction)
                {
                    // IF CAN COMBO
                    // hasPerformedCombo = true;
                    // currentAttack.comboAction.AttemptToPerformAction((aiCharacter));
                }
            }

            if (!hasPerformedAttack)
            {
                if (aICharacter.actionRecoveryTimer > 0) return this;
                if (aICharacter.isPerformingAction) return this;

                PerformAttack(aICharacter);
                return this;
            }


            if (pivotAfterAttack)
            {
                aICharacter.characterCombatManager.PivotTowardsTarget(aICharacter);
            }
            
            return SwitchState(aICharacter, aICharacter.combatStanceState);
            
        }

        protected void PerformAttack(AICharacterManager aICharacter)
        {
            hasPerformedAttack = true;
            currentAttack.AttemptToPerformAction(aICharacter);
            aICharacter.characterSoundEffectsManager.PlayAttack();
            aICharacter.actionRecoveryTimer = currentAttack.actionRecoveryTime;
        }

        protected override void ResetStateFlags(AICharacterManager aICharacter)
        {
            base.ResetStateFlags(aICharacter);
            hasPerformedAttack = false;
            hasPerformedCombo = false;
        }
    }
}