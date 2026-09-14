using Character.AICharacters.Skeleton;
using UnityEngine;

namespace Character.AICharacters.Actions
{
    [CreateAssetMenu(menuName = "Aphelion/AI/Actions/Attack")]
    public class AICharacterAttackAction : ScriptableObject
    {
        [Header("Attack")] 
        [SerializeField] private string attackAnimation;
        
        [Header("Combo Action")]
        public AICharacterAttackAction comboAction;

        [Header("Action Values")] 
        public int attackWeight;

        [SerializeField] AttackTypes attackType;
        // cna be repeated
        public float actionRecoveryTime = 1.5f;
        public float minimumAttackAngle = -35;
        public float maximumAttackAngle = 35;
        
        public float minimumAttackDistance = 0;
        public float maximumAttackDistance = 2;
        
        public void AttemptToPerformAction(CharacterManager aICharacter)
        {   
            aICharacter.characterAnimationManager.PlayTargetAttackActionAnimation(attackType, attackAnimation);
        }
    }
}