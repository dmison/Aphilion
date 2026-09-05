using System.Collections.Generic;
using Character.Player;
using UnityEngine;
using World;

namespace Character.AICharacters
{
    public class AICharacterCombatManager:  CharacterCombatManager
    {

        [SerializeField] public float attackRotationSpeed = 25;
        
        
        private static readonly int IsTurning = Animator.StringToHash("IsTurning");
        private AICharacterManager _aiCharacterManager;

        private bool _rotatingRight;
        private bool _rotatingLeft;
        
        protected override void Awake()
        {
            base.Awake();
            _aiCharacterManager = GetComponent<AICharacterManager>();
            
        }

        public override void PivotTowardsTarget(CharacterManager character)
        {
            Debug.Log("ai method");
            //lets do this without animation first
            if (!currentLockTarget) return;
            if (!_aiCharacterManager.canRotate) return;
            if (_aiCharacterManager.isPerformingAction) return;

            // hacked from the combat one below
            Vector3 targetDirection = currentLockTarget.transform.position - _aiCharacterManager.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            if(targetDirection == Vector3.zero) 
                targetDirection = _aiCharacterManager.transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            _aiCharacterManager.transform.rotation = Quaternion.Slerp(
                _aiCharacterManager.transform.rotation, 
                targetRotation, 
                Time.deltaTime * attackRotationSpeed);

            
        }

        public void RotateTowardsTargetWhilstAttacking(AICharacterManager aiCharacter)
        {
            if (currentLockTarget == null) return;
            if (!aiCharacter.canRotate) return;
            if (!aiCharacter.isPerformingAction) return;
            
            Vector3 targetDirection = currentLockTarget.transform.position - aiCharacter.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            if(targetDirection == Vector3.zero) 
                targetDirection = aiCharacter.transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            aiCharacter.transform.rotation = Quaternion.Slerp(
                aiCharacter.transform.rotation, 
                targetRotation, 
                Time.deltaTime * attackRotationSpeed);
        }
        
        public override void RotateToAngle(float angle)
        {
            if (!currentLockTarget) return;
            // if (_aiCharacterManager.isPerformingAction) return;

            Debug.Log(angle);
            if (angle > 25)
            {
                if (!_rotatingRight)
                {
                    _aiCharacterManager.characterAnimationManager.PlayTargetActionAnimation("turn_r_90_standing", true);                    
                }
                _rotatingRight = true;
                _rotatingLeft = false;
            }
            if (angle < -25)
            {
                if (!_rotatingLeft)
                {
                    _aiCharacterManager.characterAnimationManager.PlayTargetActionAnimation("turn_l_90_standing", true);
                }
                _rotatingRight = false;
                _rotatingLeft = true;
            }
            
            if (angle is <= 25 and >= -25)
            {
                _rotatingRight = false;
                _rotatingLeft = false;
            }

            _aiCharacterManager.animator.SetBool("IsTurningRight", _rotatingRight);
            _aiCharacterManager.animator.SetBool("IsTurningLeft", _rotatingLeft);

            
            
        }
        
        public override void FindValidTarget()
        {
            List<CharacterManager> targets = _aiCharacterManager.senses.CharactersInVision();
            foreach (CharacterManager target in targets)
            {
                
                if (target.CompareTag("Player"))
                {
                    PlayerManager player = target.GetComponent<PlayerManager>();
                    currentLockTarget = player;
                    break;
                }
            }
        }

        public void ReduceStaminaBasedOnAttack()
        {
            
        }
        public void OpenDamageColliders(){}
        public void CloseDamageColliders(){}
    }
}