using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using GameFlow;

public class StatesLogger : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus<OnGameStateChanged>.Subscribe(GameStateChanged);
        EventBus<OnGameStateTimerUpdated>.Subscribe(GameStateTimeUpdated);
    }

    private void GameStateChanged(object caller, OnGameStateChanged stateChanged)
    {
        Debug.Log($"Game state changed on {stateChanged.newState} with index {stateChanged.gameStateIndex}. Event source is {caller}.");
    }

    private void GameStateTimeUpdated(object caller, OnGameStateTimerUpdated timerUpdated)
    {
        //Debug.Log($"Game state time updated. State enter time is {timerUpdated.stateEnterTime}. Current state time is {timerUpdated.stateTime}. Expected state duration is {timerUpdated.stateDuration}. Event source is {caller}.");
    }

    private void OnDisable()
    {
        EventBus<OnGameStateChanged>.Unsubscribe(GameStateChanged);
        EventBus<OnGameStateTimerUpdated>.Unsubscribe(GameStateTimeUpdated);
    }
}
