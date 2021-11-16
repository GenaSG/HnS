using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateTimerGetter : MonoBehaviour
{
    [SerializeField]
    private Game game;
    [SerializeField]
    private BaseGameState TriggerOnState;
    [System.Serializable]
    public class IntUnityEvent : UnityEvent<int> { }
    [System.Serializable]
    public class StringUnityEvent : UnityEvent<string> { }
    public IntUnityEvent OnTimerUpdated;
    public StringUnityEvent OnTimerUpdatedString;
    public UnityEvent OnTimerStopped;
    public WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);
    private IEnumerator timer;

    public void StartTimer()
    {
        StopAllCoroutines();
        timer = CountDown();
        StartCoroutine(timer);
    }

    private IEnumerator CountDown()
    {
        while (game.CurrentState == TriggerOnState)
        {
            OnTimerUpdated.Invoke((int)game.StateTimeLeft);
            OnTimerUpdatedString.Invoke(((int)game.StateTimeLeft).ToString());
            yield return waitForOneSecond;
        }
        OnTimerStopped.Invoke();
    }

    private void OnEnable()
    {
        game.OnStateChanged += Game_OnStateChanged;

    }

    private void Game_OnStateChanged()
    {
        if (game.CurrentState == TriggerOnState) {
            StartTimer();
        }
    }

    private void OnDisable()
    {
        game.OnStateChanged -= Game_OnStateChanged;
    }
}
