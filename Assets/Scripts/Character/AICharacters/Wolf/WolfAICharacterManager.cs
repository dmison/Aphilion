using System.Linq;
using Character.AICharacters.States;
using UnityEngine;
using UnityEngine.AI;
using World;

namespace Character.AICharacters.Wolf
{
    public class WolfAICharacterManager: AICharacterManager
    {
        protected override void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            characterCombatManager = GetComponent<CharacterCombatManager>();
            characterSoundEffectsManager = GetComponent<CharacterSoundEffectsManager>();
            _models = GetComponentsInChildren<SkinnedMeshRenderer>(true).ToList();
            _colliders = GetComponentsInChildren<Collider>(true).ToList();

            animator = GetComponent<Animator>();
            animator.SetBool("Grounded_b", true);
            
            senses = GetComponent<CharacterSenses>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            
            idleState = Instantiate(idleState);
            pursueState = Instantiate(pursueState);
            // combatStanceState = Instantiate(combatStanceState);
            // attackState = Instantiate(attackState);
            //
            defaultState = Instantiate(defaultState);

        }
    }
}