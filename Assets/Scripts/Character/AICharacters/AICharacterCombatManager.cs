using System.Collections.Generic;
using Character.Player;
using UnityEngine;
using World;

namespace Character.AICharacters
{
    public class AICharacterCombatManager:  CharacterCombatManager
    {
        [Header("Target Info")]
        public float distanceFromTarget;
        public float viewableAngle;
        
        private static readonly int IsTurning = Animator.StringToHash("IsTurning");
        private AICharacterManager _aiCharacterManager;

        private bool _rotatingRight;
        private bool _rotatingLeft;
        
        protected override void Awake()
        {
            base.Awake();
            _aiCharacterManager = GetComponent<AICharacterManager>();
            
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
    }
}