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
//
//Player
public struct OnPlayerObjectSpawned
{
    public uint netID;
    public GameObject playerObject;
    public bool isLocalPlayer;
    public bool isClient;
    public bool isServer;
}

public struct OnPlayerObjectDestroyed
{
    public uint netID;
    public GameObject playerObject;
    public bool isLocalPlayer;
    public bool isClient;
    public bool isServer;
}
public struct OnPlayerDied
{
    public uint netID;
    public GameObject playerObject;
    public bool isLocalPlayer;
    public bool isClient;
    public bool isServer;
}

public struct OnCameraProfileUpdated
{
    public CameraFollowProfile profile;
}

public struct OnPropSelected
{
    public GameObject prop;
}

public struct OnSelectedBy
{
    public GameObject user;
}
//
//Teams
public struct OnSpectatorsUpdated
{
    public HashSet<uint> spectators;
}

public struct OnHidersUpdated
{
    public HashSet<uint> hiders;
}

public struct OnSeekersUpdated
{
    public HashSet<uint> seekers;
}
//
//Physics
[System.Serializable]
public enum CollisionState { OnEnter, OnExit}
public struct OnCollisionChanged
{
    public CollisionState state;
    public Collision collision;
}

public struct OnTriggerChanged
{
    public CollisionState state;
    public Collider collider;
}
//
