using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [System.Serializable]
    public class IntUnityEvent : UnityEvent<int> { }
    [System.Serializable]
    public class StringUnityEvent : UnityEvent<string> { }
    public IntUnityEvent OnTimerUpdated;
    public StringUnityEvent OnTimerUpdatedString;
    public UnityEvent OnTimerStopped;
    public WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);
    private IEnumerator timer;
    public void StartTimer(int seconds)
    {
        StopAllCoroutines();
        timer = CountDown(seconds);
        StartCoroutine(timer);
    }

    private IEnumerator CountDown(int seconds)
    {
        int counter = seconds;
        while(counter > 0)
        {
            OnTimerUpdated.Invoke(counter);
            OnTimerUpdatedString.Invoke(counter.ToString());
            counter--;
            yield return waitForOneSecond;
        }
        OnTimerStopped.Invoke();

    }
}
