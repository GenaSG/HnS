using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName = "Game/GameInstance")]
public class Game : ScriptableObject
{

    [SerializeField]
    private BaseGameState currentState;
    public BaseGameState CurrentState { get { return currentState; } }
    public event Action OnStateChanged = delegate {};
    private int currentStateID = -1;
    [SerializeField]
    private List<BaseGameState> availableStates = new List<BaseGameState>();
    //Context variables
    public double StateEnterTime;
    public double StateElapsedTime;
    public double StateTimeLeft;

    public void Start()
    {
        SwitchState(availableStates[0]);
    }

    public void Update()
    {
        currentState.OnStateUpdate(this);
    }

    public void Stop()
    {
        currentState.OnStateExit(this);
    }



    public void SwitchState(BaseGameState newState)
    {
        if(!availableStates.Contains(newState))
        {
            Debug.LogError("New state " + newState + " not found in available states list");
            return;
        }
        if(currentState != null)currentState.OnStateExit(this);
        newState.OnStateEnter(this);
        currentStateID = availableStates.IndexOf(newState);
        currentState = newState;
        OnStateChanged();
    }

    public void SwitchState(int newStateID)
    {
        if(!(newStateID >=0 && newStateID < availableStates.Count))
        {
            Debug.LogError("New state ID" + newStateID + " not found in available states list");
            return;
        }
        if (currentStateID == newStateID) return;
        var newState = availableStates[newStateID];
        currentState.OnStateExit(this);
        newState.OnStateEnter(this);
        currentStateID = newStateID;
        currentState = newState;
        OnStateChanged();
    }

    public int GetCurrentStateID()
    {
        return currentStateID;
    }
}
