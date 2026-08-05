using Character;
using UnityEngine;

namespace Animation
{
    public class ResetActionFlag : StateMachineBehaviour
    {
        private CharacterManager _characterManager;

        private void ResetFlag(Animator animator)
        {
            if (_characterManager == null)
            {
                _characterManager = animator.GetComponent<CharacterManager>();
            }
            _characterManager.isPerformingAction = false;
            _characterManager.canRotate = true;
            _characterManager.canMove = true;
            animator.applyRootMotion = false;
        }
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ResetFlag(animator);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // ResetFlag(animator);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // ResetFlag(animator);
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
