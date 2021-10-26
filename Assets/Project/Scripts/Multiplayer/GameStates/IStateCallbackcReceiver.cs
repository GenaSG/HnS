using UnityEngine;

public interface IStateCallbackcReceiver
{
    void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

    void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

    void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

    void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

    void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

}
