using UnityEngine;
using Mirror;
using SimpleEventBus;
using System;
using GameFlow;

public class NetworkedMapGeneration : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnSeedUpdated))]
    private uint seed;
    [SerializeField]
    private GameState markerState;


    private void OnEnable()
    {
        EventBus<OnGameStateChanged>.Subscribe(GameStateChanged);
    }

    private void GameStateChanged(object caller, OnGameStateChanged stateChanged)
    {
        if (!isServer) return;
        if (markerState == null || stateChanged.newState != markerState) return;
        seed = (uint)UnityEngine.Random.Range(0, 2147483640);
        EventBus<OnMapSeedGenerated>.Raise(this, new OnMapSeedGenerated { seed = seed });
    }

    private void OnDisable()
    {
        EventBus<OnGameStateChanged>.Unsubscribe(GameStateChanged);
    }

    [ClientCallback]
    void OnSeedUpdated(uint lastSeed, uint currentSeed)
    {
        EventBus<OnMapSeedGenerated>.Raise(this, new OnMapSeedGenerated { seed = currentSeed });
    }

}
