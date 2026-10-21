using UnityEngine;

namespace Character
{
    public class CharacterAnimationManager: MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("horizontal");
        private static readonly int Vertical = Animator.StringToHash("vertical");
        protected CharacterManager _characterManager;

        [Header("Damage Animations")] 
        public string A_Hit_F_React_Sword = "A_Hit_F_React_Sword";
        public string A_Hit_B_React_Sword = "A_Hit_B_React_Sword";
        public string A_Hit_L_React_Sword = "A_Hit_L_React_Sword";
        public string A_Hit_R_React_Sword = "A_Hit_R_React_Sword";
        
        protected virtual void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
        }
        
        public virtual void UpdateAnimatorMovementParameters(float horizontal, float vertical)
        {
            _characterManager.animator.SetFloat(Horizontal, horizontal, 0.1f, Time.deltaTime);
            _characterManager.animator.SetFloat(Vertical, vertical, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(
            string targetAnimation, 
            bool isPerformingAction = true, 
            bool applyRootMotion = true,
            bool canMove = false,
            bool canRotate = false,
            float crossFade = 0.2f)
        {
            
            _characterManager.animator.applyRootMotion = applyRootMotion;
            _characterManager.isPerformingAction = isPerformingAction;
            _characterManager.canRotate = canRotate;
            _characterManager.canMove = canMove;
            
            _characterManager.animator.CrossFade(targetAnimation, crossFade);
        }
        
        public virtual void PlayTargetAttackActionAnimation(
            AttackTypes attackType,
            string targetAnimation, 
            bool isPerformingAction = true, 
            bool applyRootMotion = true,
            bool canMove = false,
            bool canRotate = false)
        {
            // track last attack for combos
            // track current action type (heavy, light, etc)
            // update animation set to current weapon animations
            // decide if animation can be parried
            // set 'isAttacking' flag for counters etc
            
            _characterManager.animator.applyRootMotion = applyRootMotion;
            _characterManager.isPerformingAction = isPerformingAction;
            _characterManager.canRotate = canRotate;
            _characterManager.canMove = canMove;
            
            _characterManager.animator.CrossFade(targetAnimation, 0.2f);
        }
        
    }
}