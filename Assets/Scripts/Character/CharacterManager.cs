using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Player;
using UnityEngine;
using UnityEngine.UIElements;

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
        private List<SkinnedMeshRenderer> _models = new List<SkinnedMeshRenderer>();
        // FLAGS
        public bool isPerformingAction = false;
        public bool canRotate = true;
        public bool canMove = true;
        public bool isGrounded = true;

        public bool IsDead => characterStatsManager.CurrentHealth <= 0;
        
        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            characterCombatManager = GetComponent<CharacterCombatManager>();
            characterSoundEffectsManager = GetComponent<CharacterSoundEffectsManager>();
            _models = GetComponentsInChildren<SkinnedMeshRenderer>(true).ToList();
            
            animator = GetComponent<Animator>();
            
        }

        public void EnableOutline(bool enable)
        {
            foreach (SkinnedMeshRenderer mesh in _models)
            {
                mesh.gameObject.layer = LayerMask.NameToLayer(enable ? "Outlined" : "Default");
            }            
        }
        
        protected virtual void Update(){}
        protected virtual void LateUpdate(){}
        protected virtual void FixedUpdate(){}
        
        public virtual IEnumerator HandleDeath(bool manuallySelectAnimation=false)
        {
            EnableOutline(false);
            characterAnimationManager.PlayTargetActionAnimation("Dead_01");
            yield return new WaitForSeconds(1);
            
            // PLAY SOME DEATH SFX
            // IF ENEMY THEN REWARD PLAYER
            // DISABLE CHARACTER
            
        }
    }
}
