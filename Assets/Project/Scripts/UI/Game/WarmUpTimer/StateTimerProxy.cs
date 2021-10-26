using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateTimerProxy : MonoBehaviour
{
    public float TimeLeft { get { return TimerState.TimeLeft; } }
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }
    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }
    public UnityEvent OnSecondPassed;
    public IntEvent OnTimerUpdated;
    public StringEvent OnTimerUpdateString;


    private void OnEnable()
    {
        TimerState.OnSecondPassed += TimerState_OnSecondPassed;
    }

    private void TimerState_OnSecondPassed()
    {
        OnSecondPassed.Invoke();
        OnTimerUpdated.Invoke((int)TimerState.TimeLeft);
        OnTimerUpdateString.Invoke(((int)TimerState.TimeLeft).ToString());
    }
    private void OnDisable()
    {
        TimerState.OnSecondPassed -= TimerState_OnSecondPassed;
    }
}
