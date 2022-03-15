using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EJoinGame { }

public struct EHostGame { }


// Map generation
public struct OnMapSeedGenerated
{
    public uint seed;
}

public struct OnMapGenerated
{
    public GameObject[] props;
    public GameObject[] levelGeometry;
}

public struct OnMapCleared { }
//
//GameFlow

public struct OnGameStateChanged
{
    public GameFlow.GameState newState;
    public uint gameStateIndex;
}

public struct OnGameStateTimerUpdated
{
    public double stateEnterTime;
    public double stateTime;
    public double stateDuration;
}
