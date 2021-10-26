using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class TimerState : StateMachineBehaviour
{
    private float startTime;
    [SerializeField]
    float stateTimerInSeconds = 30f;
    //float timeElapsed = 0f;
    [SerializeField]
    string OnDoneTriggerName = "NextState";
    [SerializeField]
    string StartTimeValueName = "StateStartTime";
    [SerializeField]
    static float timeLeft;
    public static float TimeLeft { get { return timeLeft; } }
    public static float StateTimerInSeconds { get; private set; }
    public static event Action OnSecondPassed = delegate { };
    private int lastSecond;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (NetworkServer.active)
        {
            startTime = (float)NetworkTime.time;
            animator.SetFloat(StartTimeValueName, startTime);
        }
        startTime = animator.GetFloat(StartTimeValueName);
        timeLeft = stateTimerInSeconds;
        StateTimerInSeconds = stateTimerInSeconds;
        lastSecond = (int)((startTime + stateTimerInSeconds) - (float)NetworkTime.time);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //timeElapsed += Time.deltaTime;
        if (startTime > NetworkTime.time) return; //NetworkTime not synced on client
        timeLeft = (startTime + stateTimerInSeconds) - (float)NetworkTime.time;
        if ((int)timeLeft != lastSecond) OnSecondPassed();
        lastSecond = (int)timeLeft;
        if ((startTime + stateTimerInSeconds) < NetworkTime.time && NetworkServer.active) animator.GetComponent<NetworkAnimator>().SetTrigger(OnDoneTriggerName);
    }

}
