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

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (!aiCharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }
            
            if(aiCharacter.characterCombatManager.currentLockTarget.IsDead)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }
            // ROTATE TOWARDS CHARACTER WHILST ATTACKING
            aiCharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 0);
            
            
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
                if (aiCharacter.actionRecoveryTimer > 0) return this;
                if (aiCharacter.isPerformingAction) return this;

                PerformAttack(aiCharacter);
                return this;
            }


            if (pivotAfterAttack)
            {
                aiCharacter.characterCombatManager.PivotTowardsTarget(aiCharacter);
            }
            
            return SwitchState(aiCharacter, aiCharacter.combatStanceState);
            
        }

        protected void PerformAttack(AICharacterManager aiCharacter)
        {
            hasPerformedAttack = true;
            currentAttack.AttemptToPerformAction(aiCharacter);
            aiCharacter.actionRecoveryTimer = currentAttack.actionRecoveryTime;
        }

        protected override void ResetStateFlags(AICharacterManager aiCharacter)
        {
            base.ResetStateFlags(aiCharacter);
            hasPerformedAttack = false;
            hasPerformedCombo = false;
        }
    }
}