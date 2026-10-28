using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [HideInInspector] public CharacterDeathManager characterDeathManager;
        
        protected List<SkinnedMeshRenderer> _models = new List<SkinnedMeshRenderer>();

        protected List<Collider> _colliders = new List<Collider>();
        // FLAGS
        public bool isPerformingAction = false;
        public bool canRotate = true;
        public bool canMove = true;
        public bool isGrounded = true;

        [Header("Action Recovery")]
        public float actionRecoveryTimer = 0;
        
        public bool IsDead => characterStatsManager.CurrentHealth <= 0;
        public bool isMoving = false;

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
        
        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            characterCombatManager = GetComponent<CharacterCombatManager>();
            characterSoundEffectsManager = GetComponent<CharacterSoundEffectsManager>();
            characterDeathManager = GetComponent<CharacterDeathManager>();
            _models = GetComponentsInChildren<SkinnedMeshRenderer>(true).ToList();
            _colliders = GetComponentsInChildren<Collider>(true).ToList();
            animator = GetComponent<Animator>();
            animator.SetBool("isGrounded", true);
        }

        public void EnableOutline(bool enable)
        {
            if (IsDead) return;
            foreach (SkinnedMeshRenderer mesh in _models)
            {
                mesh.gameObject.layer = LayerMask.NameToLayer(enable ? "Outlined" : "Default");
            }            
        }

        protected virtual void Update()
        {
            HandleActionRecovery();
        }
        protected virtual void LateUpdate(){}
        protected virtual void FixedUpdate(){}
        
        public virtual IEnumerator HandleDeath(bool manuallySelectAnimation=false)
        {
            EnableOutline(false);
            // PLAY SOME DEATH SFX
            characterSoundEffectsManager.PlayDie();
            characterAnimationManager.PlayTargetActionAnimation("Dead_01");
            
            // DISABLE CHARACTER
            foreach (var collider1 in _colliders)
            {
                collider1.enabled = false;
            }
            
            yield return new WaitForSeconds(1);
            
            // IF ENEMY THEN REWARD PLAYER
            
            
        }
    }
}
