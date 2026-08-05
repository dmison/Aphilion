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
            animator = GetComponent<Animator>();
            DontDestroyOnLoad(this);
        }
    
        protected virtual void Update(){}
        protected virtual void LateUpdate(){}

        public virtual IEnumerator HandleDeath(bool manuallySelectAnimation=false)
        {
            characterAnimationManager.PlayTargetActionAnimation("Dead_01");
            PlayerUIManager.instance.deathMessageManager.ShowDeathMessage();
            
            yield return new WaitForSeconds(5);
            
            // PLAY SOME DEATH SFX
            // IF ENEMY THEN REWARD PLAYER
            // DISABLE CHARACTER
            
        }
    }
}
