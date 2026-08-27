
using UnityEngine;
using UnityEngine.AI;

namespace Character.AICharacters
{
    public class AICharacterManager: CharacterManager
    {
        [Header("AI STATES")]
        [SerializeField] public AIState currentState;
        [SerializeField] public AIState idleState;
        [SerializeField] public AIState pursueState;
        
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
        
        private void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }

            if (navMeshAgent.enabled)
            {
                animator.SetBool("IsMoving", navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
            
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ProcessStateMachine();
        }
    }
}