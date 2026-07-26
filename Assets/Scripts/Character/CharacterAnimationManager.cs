using UnityEngine;

namespace Character
{
    public class CharacterAnimationManager: MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("horizontal");
        private static readonly int Vertical = Animator.StringToHash("vertical");
        private CharacterManager _characterManager;

        protected virtual void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
        }
        
        public void UpdateAnimatorMovementParameters(float horizontal, float vertical)
        {
            _characterManager.animator.SetFloat(Horizontal, horizontal, 0.1f, Time.deltaTime);
            _characterManager.animator.SetFloat(Vertical, vertical, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(
            string targetAnimation, 
            bool isPerformingAction = true, 
            bool applyRootMotion = true,
            bool canMove = false,
            bool canRotate = false)
        {
            
            _characterManager.animator.applyRootMotion = applyRootMotion;
            _characterManager.isPerformingAction = isPerformingAction;
            _characterManager.canRotate = canRotate;
            _characterManager.canMove = canMove;
            
            _characterManager.animator.CrossFade(targetAnimation, 0.2f);
            
            
        }
        
    }
}