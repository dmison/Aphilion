using System.Collections.Generic;
using Character.AICharacters.Actions;
using UnityEngine;

namespace Character.AICharacters.States
{
    [CreateAssetMenu(menuName = "Aphelion/AI/States/Combat Stance")]
    public class CombatStanceState: AIState
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        [Header("Attacks")] 
        public List<AICharacterAttackAction> aiCharacterAttacks;
        protected List<AICharacterAttackAction> potentialAttacks;
        private AICharacterAttackAction chosenAttack;
        private AICharacterAttackAction previousAttack;
        private bool hasAttack = false;
        
        [Header("Combo")] 
        [SerializeField] protected bool canPerformCombo;

        [SerializeField] protected int changeToPerformCombo = 25;
        protected bool hasRolledForComboChance = false;
        
        [Header("Engagement Distance")]
        [SerializeField] protected float maxEngagementDistance = 5;

        public override AIState Tick(AICharacterManager aICharacter)
        {
            if (aICharacter.isPerformingAction) return this;
            if (!aICharacter.navMeshAgent.enabled)
            {
                aICharacter.navMeshAgent.enabled = true;
            }

            
            if (!aICharacter.isMoving)
            {
                if (aICharacter.characterCombatManager.viewableAngle is < -30 or > 30)
                {
                    aICharacter.characterCombatManager.PivotTowardsTarget(aICharacter);
                    // turn to face target
                }    
            }
            
            aICharacter.characterCombatManager.RotateTowardsAgent(aICharacter.navMeshAgent.transform);
            
            // lost target: go back to idle
            if (!aICharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aICharacter, aICharacter.idleState);
            }

            if (!hasAttack)
            {
                GetNewAttack(aICharacter);
                
            }
            else
            {
                aICharacter.attackState.currentAttack = chosenAttack;
                // TODO: ROLL FOR COMBO CHANCE
                return SwitchState(aICharacter, aICharacter.attackState);
                
            }

            // if too far away then pursue
            if (aICharacter.characterCombatManager.distanceFromTarget > maxEngagementDistance)
            {
                SwitchState(aICharacter, aICharacter.pursueState);
            }
            
            // otherwise ensure we are closing towards target
            aICharacter.animator.SetBool(IsMoving, true);
            aICharacter.navMeshAgent.SetDestination(aICharacter.characterCombatManager.currentLockTarget.transform.position);
            aICharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 1);
            return this;
        }

        protected virtual void GetNewAttack(AICharacterManager aICharacter)
        {
            potentialAttacks = new List<AICharacterAttackAction>();
            foreach (var attack in aiCharacterAttacks)
            {
                // range check
                if(attack.maximumAttackDistance < aICharacter.characterCombatManager.distanceFromTarget) continue;
                if (attack.minimumAttackDistance > aICharacter.characterCombatManager.distanceFromTarget) continue;
                
                // angle check
                if(attack.maximumAttackAngle < aICharacter.characterCombatManager.viewableAngle) continue;
                if(attack.minimumAttackAngle > aICharacter.characterCombatManager.viewableAngle) continue;
                
                potentialAttacks.Add(attack);
            }

            if (potentialAttacks.Count <= 0) return;

            int totalWeight = 0;
            foreach (AICharacterAttackAction attack in potentialAttacks)
            {
                totalWeight += attack.attackWeight;
            }

            int randomWeightValue = Random.Range(0, totalWeight + 1);
            int processedWeight = 0;

            foreach (AICharacterAttackAction attack in potentialAttacks)
            {
                processedWeight += attack.attackWeight;
                if (randomWeightValue <= processedWeight)
                {
                    previousAttack = chosenAttack;
                    chosenAttack = attack;
                    hasAttack = true;
                    return;
                }
            }


        }
        
        protected virtual bool RollForComboChance(int outcomeChance)
        {
            return Random.Range(0, 100) < outcomeChance;
        }

        protected override void ResetStateFlags(AICharacterManager aICharacter)
        {
            base.ResetStateFlags(aICharacter);
            hasRolledForComboChance = false;
            hasAttack = false;
        }
    }
}