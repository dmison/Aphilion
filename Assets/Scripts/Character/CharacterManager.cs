using System.Collections;
using Character.Player;
using UnityEngine;

namespace Character
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterStatsManager characterStatsManager;
        [HideInInspector] public CharacterAnimationManager characterAnimationManager;
        [HideInInspector] public CharacterEffectsManager characterEffectsManager;
        [HideInInspector] public CharacterCombatManager characterCombatManager;
        [HideInInspector] public CharacterSoundEffectsManager characterSoundEffectsManager;
        
        // FLAGS
        public bool isPerformingAction = false;
        public bool canRotate = true;
        public bool canMove = true;
        public bool isGrounded = true;

        public bool isDead => characterStatsManager.CurrentHealth <= 0;
        
        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            characterCombatManager = GetComponent<CharacterCombatManager>();
            characterSoundEffectsManager = GetComponent<CharacterSoundEffectsManager>();
            
            animator = GetComponent<Animator>();
            
        }
    
        protected virtual void Update(){}
        protected virtual void LateUpdate(){}

        public virtual IEnumerator HandleDeath(bool manuallySelectAnimation=false)
        {
            characterAnimationManager.PlayTargetActionAnimation("Dead_01");
            yield return new WaitForSeconds(1);
            
            // PLAY SOME DEATH SFX
            // IF ENEMY THEN REWARD PLAYER
            // DISABLE CHARACTER
            
        }
    }
}
