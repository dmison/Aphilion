using System;
using Character.AICharacters.Skeleton;
using Character.AICharacters.States;
using UnityEngine;
using UnityEngine.AI;
using World;

namespace Character.AICharacters
{
    public class AICharacterManager: CharacterManager
    {
        protected static readonly int IsTurning = Animator.StringToHash("IsTurning");
        protected static readonly int IsMoving = Animator.StringToHash("IsMoving");
        
        [Header("AI STATES")]
        [SerializeField] public IdleState idleState;
        [SerializeField] public PursueTargetState pursueState;
        [SerializeField] public CombatStanceState combatStanceState;
        [SerializeField] public AttackState attackState;

        [SerializeField] public AIState defaultState;
        public AIState currentState;
        
        public NavMeshAgent navMeshAgent;
        public CharacterSenses senses;

        protected override void Awake()
        { 
            base.Awake();
            senses = GetComponent<CharacterSenses>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            
            idleState = Instantiate(idleState);
            pursueState = Instantiate(pursueState);
            combatStanceState = Instantiate(combatStanceState);
            attackState = Instantiate(attackState);
            
            currentState = Instantiate(defaultState);
            
        }
        
        
        protected virtual void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }

            if (characterCombatManager.currentLockTarget != null)
            {
                characterCombatManager.viewableAngle = WorldUtilityManager.Instance.GetAngleToTarget(transform, characterCombatManager.DirectionToTarget);
                characterCombatManager.distanceFromTarget = Vector3.Distance(transform.position, characterCombatManager.currentLockTarget.transform.position);
            }
            
            if (navMeshAgent.enabled)
            {
                isMoving = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;
            }
            else
            {
                isMoving = false;
            }
            animator.SetBool(IsMoving, isMoving);

        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ProcessStateMachine();
        }
    }
}