using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WarmUpState : StateMachineBehaviour
{
    [SerializeField]
    string StateStartTimeName = "StateStartTime";
    [SerializeField]
    string OnDoneTriggerName = "NextState";
    float startTime;
    [SerializeField]
    float StateTimeInSeconds = 15f;
    [SerializeField]
    GameObject timerPrefab;
    GameObject timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //networkManager = networkManagerGameObject.Object.GetComponent<NetworkManager>();
        if (NetworkServer.active)
        {
            animator.SetFloat(StateStartTimeName, (float)NetworkTime.time);
        }
        startTime = animator.GetFloat(StateStartTimeName);
        if(NetworkServer.active)
        {
            //Spawn Timer
            timer = Instantiate(timerPrefab);
            timer.SendMessage("StartTimer", StateTimeInSeconds - (NetworkTime.time - startTime), SendMessageOptions.DontRequireReceiver);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(!NetworkServer.active && NetworkClient.active)
        {
            if (NetworkTime.offset > -Mathf.Epsilon) return; //NetworkTime isn't synced yet
        }

        if (timer == null)
        {
            timer = Instantiate(timerPrefab);
            timer.SendMessage("StartTimer", StateTimeInSeconds - (NetworkTime.time - startTime), SendMessageOptions.DontRequireReceiver);
        }

        if (StateTimeInSeconds - (NetworkTime.time - startTime) > 0) return;

        if (NetworkServer.active) animator.GetComponent<NetworkAnimator>().SetTrigger(OnDoneTriggerName);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(timer);
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
