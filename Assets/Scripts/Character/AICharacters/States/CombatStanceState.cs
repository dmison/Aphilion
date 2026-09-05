using System.Collections.Generic;
using Character.AICharacters.Actions;
using Unity.IntegerTime;
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

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction) return this;
            if (!aiCharacter.navMeshAgent.enabled)
            {
                aiCharacter.navMeshAgent.enabled = true;
            }

            
            if (!aiCharacter.isMoving)
            {
                if (aiCharacter.characterCombatManager.viewableAngle is < -30 or > 30)
                {
                    aiCharacter.characterCombatManager.PivotTowardsTarget(aiCharacter);
                    // turn to face target
                }    
            }
            
            aiCharacter.characterCombatManager.RotateTowardsAgent(aiCharacter.navMeshAgent.transform);
            
            // lost target: go back to idle
            if (!aiCharacter.characterCombatManager.currentLockTarget)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }

            if (!hasAttack)
            {
                GetNewAttack(aiCharacter);
                
            }
            else
            {
                aiCharacter.attackState.currentAttack = chosenAttack;
                // TODO: ROLL FOR COMBO CHANCE
                return SwitchState(aiCharacter, aiCharacter.attackState);
                
            }

            // if too far away then pursue
            if (aiCharacter.characterCombatManager.distanceFromTarget > maxEngagementDistance)
            {
                SwitchState(aiCharacter, aiCharacter.pursueState);
            }
            
            // otherwise ensure we are closing towards target
            aiCharacter.animator.SetBool(IsMoving, true);
            aiCharacter.navMeshAgent.SetDestination(aiCharacter.characterCombatManager.currentLockTarget.transform.position);
            aiCharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0, 1);
            return this;
        }

        protected virtual void GetNewAttack(AICharacterManager aiCharacter)
        {
            potentialAttacks = new List<AICharacterAttackAction>();
            foreach (var attack in aiCharacterAttacks)
            {
                // range check
                if(attack.maximumAttackDistance < aiCharacter.characterCombatManager.distanceFromTarget) continue;
                if (attack.minimumAttackDistance > aiCharacter.characterCombatManager.distanceFromTarget) continue;
                
                // angle check
                if(attack.maximumAttackAngle < aiCharacter.characterCombatManager.viewableAngle) continue;
                if(attack.minimumAttackAngle > aiCharacter.characterCombatManager.viewableAngle) continue;
                
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

        protected override void ResetStateFlags(AICharacterManager aiCharacter)
        {
            base.ResetStateFlags(aiCharacter);
            hasRolledForComboChance = false;
            hasAttack = false;
        }
    }
}