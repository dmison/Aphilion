using UnityEngine;

namespace Character.AICharacters.Wolf
{
    public class WolfAICharacterAnimationManager: CharacterAnimationManager
    {
        public override void UpdateAnimatorMovementParameters(float horizontal, float vertical)
        {
            _characterManager.animator.SetFloat("Movement_f", vertical, 0.1f, Time.deltaTime);
               
        }
    }
}