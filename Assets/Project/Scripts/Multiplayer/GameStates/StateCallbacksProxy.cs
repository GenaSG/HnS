using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCallbacksProxy : StateMachineBehaviour
{
    
    internal class NullReceiver : IStateCallbackcReceiver
    {
        public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
    IStateCallbackcReceiver receiver = new NullReceiver();
    [SerializeField]
    FindGameObject find = new FindGameObject();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(find.Object.TryGetComponent<IStateCallbackcReceiver>(out IStateCallbackcReceiver _receiver) ){
            receiver = _receiver;
        }
        receiver.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        receiver.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        receiver.OnStateExit(animator, stateInfo, layerIndex);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
        receiver.OnStateMove(animator, stateInfo, layerIndex);
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
        receiver.OnStateIK(animator, stateInfo, layerIndex);
    }
}
