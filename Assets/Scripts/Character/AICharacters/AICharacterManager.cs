
using Character.AICharacters.States;
using UnityEngine;
using UnityEngine.AI;
using World;

namespace Character.AICharacters
{
    public class AICharacterManager: CharacterManager
    {
        private static readonly int IsTurning = Animator.StringToHash("IsTurning");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        
        
        
        [Header("AI STATES")]
        [SerializeField] public AIState currentState;
        [SerializeField] public IdleState idleState;
        [SerializeField] public PursueTargetState pursueState;
        [SerializeField] public CombatStanceState combatStanceState;
        [SerializeField] public AttackState attackState;
        
        [Header("Action Recovery")]
        public float actionRecoveryTimer = 0;
        
        public NavMeshAgent navMeshAgent;
        
        public CharacterSenses senses;
        
        protected override void Awake()
        {
            base.Awake();
            senses = GetComponent<CharacterSenses>();
            characterCombatManager = GetComponent<AICharacterCombatManager>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            idleState = Instantiate(idleState);
            pursueState = Instantiate(pursueState);
            currentState = Instantiate(currentState);
        }

        public void HandleActionRecovery()
        {
            if (actionRecoveryTimer > 0)
            {
                if (!isPerformingAction)
                {
                    actionRecoveryTimer -= Time.deltaTime;
                }
            }
        }
        
        private void ProcessStateMachine()
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
            
            // else
            // {
            //     animator.SetBool("IsTurningRight", false);
            //     animator.SetBool("IsTurningLeft", false);
            // }
            //




        }
        


        protected override void Update()
        {
            base.Update();
            HandleActionRecovery();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ProcessStateMachine();
        }
    }
}