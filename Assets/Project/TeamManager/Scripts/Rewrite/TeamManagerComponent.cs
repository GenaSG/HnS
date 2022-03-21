using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using Mirror;
using GameFlow;
using System;
using System.Linq;

public class TeamManagerComponent : NetworkBehaviour
{
    public struct UintArrayContainer
    {
        public uint[] value;
        public UintArrayContainer(int size)
        {
            value = new uint[size];
        }
    }

    [SerializeField]
    private GameState createTeamsGameState;
    [SerializeField]
    private GameState destroyTeamsGameState;
    [SerializeField]
    private uint defaultSeekersCount = 1;
    [SyncVar(hook = nameof(OnSyncSpectatorIDs))]
    private UintArrayContainer syncSpectatorIDs;
    [SyncVar(hook = nameof(OnSyncHiderIDs))]
    private UintArrayContainer syncHiderIDs;
    [SyncVar(hook = nameof(OnSyncSeekerIDs))]
    private UintArrayContainer syncSeekerIDs;

    [SerializeField]
    public HashSet<uint> spectators = new HashSet<uint>();
    [SerializeField]
    public HashSet<uint> hiders = new HashSet<uint>();
    [SerializeField]
    public HashSet<uint> seekers = new HashSet<uint>();

    private void OnEnable()
    {
        EventBus<OnPlayerObjectSpawned>.Subscribe(PlayerObjectSpawned);
        EventBus<OnPlayerObjectDestroyed>.Subscribe(PlayerObjectDestroyed);
        EventBus<OnGameStateChanged>.Subscribe(GameStateChanged);
    }
    /// <summary>
    /// Reacts on recieved network update for spectators hashset. Called on client.
    /// </summary>
    /// <param name="last"></param>
    /// <param name="current"></param>
    private void OnSyncSpectatorIDs(UintArrayContainer last, UintArrayContainer current)
    {
        if (!isClient || isServer) return;
        syncSpectatorIDs = current;
        spectators = new HashSet<uint>(current.value);
        NotifySpectators();
    }
    /// <summary>
    /// Reacts on recieved network update for hiders hashset. Called on client.
    /// </summary>
    /// <param name="last"></param>
    /// <param name="current"></param>
    private void OnSyncHiderIDs(UintArrayContainer last, UintArrayContainer current)
    {
        if (!isClient || isServer) return;
        syncHiderIDs = current;
        hiders = new HashSet<uint>(current.value);
        NotifyHiders();
    }
    /// <summary>
    /// Reacts on recieved network update for seekers hashset. Called on client.
    /// </summary>
    /// <param name="last"></param>
    /// <param name="current"></param>
    private void OnSyncSeekerIDs(UintArrayContainer last, UintArrayContainer current)
    {
        if (!isClient || isServer) return;
        syncSeekerIDs = current;
        seekers = new HashSet<uint>(current.value);
        NotifySeekers();
    }


    private void OnDisable()
    {
        EventBus<OnPlayerObjectSpawned>.Unsubscribe(PlayerObjectSpawned);
        EventBus<OnPlayerObjectDestroyed>.Unsubscribe(PlayerObjectDestroyed);
        EventBus<OnGameStateChanged>.Unsubscribe(GameStateChanged);

    }
    /// <summary>
    /// Adds new player ID to spectators hashset. Called on server.
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="objectSpawned"></param>
    private void PlayerObjectSpawned(object caller, OnPlayerObjectSpawned objectSpawned)
    {
        if (!isServer) return;
        if (spectators.Contains(objectSpawned.netID)) return;
        spectators.Add(objectSpawned.netID);
        syncSpectatorIDs = CopyToUintArray(spectators);
        NotifySpectators();
        //Debug.Log($"{this} adding player ID {objectSpawned.netID} to spectators");
    }
    /// <summary>
    /// Removes player ID from all hashsets. Called on server.
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="objectDestroyed"></param>
    private void PlayerObjectDestroyed(object caller, OnPlayerObjectDestroyed objectDestroyed)
    {
        if (!isServer) return;
        if (spectators.Contains(objectDestroyed.netID))
        {
            spectators.Remove(objectDestroyed.netID);
            syncSpectatorIDs = CopyToUintArray(spectators);
            NotifySpectators();
        }
        if (hiders.Contains(objectDestroyed.netID))
        {
            hiders.Remove(objectDestroyed.netID);
            syncHiderIDs = CopyToUintArray(hiders);
            NotifyHiders();
        }
        //TODO: Need to handle case when the only seeker disconnected
        if (seekers.Contains(objectDestroyed.netID))
        {
            seekers.Remove(objectDestroyed.netID);
            syncSeekerIDs = CopyToUintArray(seekers);
            NotifySeekers();
        }
    }
    /// <summary>
    /// Reacts on game state change. Called on server.
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="stateChanged"></param>
    private void GameStateChanged(object caller, OnGameStateChanged stateChanged)
    {
        if (!isServer) return;
        //Debug.Log($"{this} calling GameStateChanged");
        if (createTeamsGameState != null && stateChanged.newState == createTeamsGameState) CreateTeams();
        if (destroyTeamsGameState != null && stateChanged.newState == destroyTeamsGameState) ClearTeams();
    }
    /// <summary>
    /// Clears teams. Called on server.
    /// </summary>
    private void ClearTeams()
    {
        spectators.UnionWith(seekers);
        spectators.UnionWith(hiders);
        seekers = new HashSet<uint>();
        hiders = new HashSet<uint>();
        SyncTeams();
        NotifyAll();
    }
    /// <summary>
    /// Creates teams. Selects rundom ID from spectators and updates hashsets. Called on server.
    /// </summary>
    private void CreateTeams()
    {
        if (spectators.Count < defaultSeekersCount) return;
        seekers = new HashSet<uint>();
        hiders = new HashSet<uint>();

        for(int i = 0; i < defaultSeekersCount; i++)
        {
            uint seekerID = spectators.First();
            spectators.Remove(seekerID);
            seekers.Add(seekerID);
            //Debug.Log($"Selecting seeker. Seeker id is {seekerID}");
        }
        var tmp = new uint[spectators.Count];
        spectators.CopyTo(tmp);
        hiders = new HashSet<uint>(tmp);
        spectators = new HashSet<uint>();
        SyncTeams();
        NotifyAll();
        //Debug.Log($"{this} Spectators count is {spectators.Count}");
        //Debug.Log($"{this} Hiders count is {hiders.Count}");
        //Debug.Log($"{this} Seekers count is {seekers.Count}");
    }

    /// <summary>
    /// Sets new values to sync vars. Called on server.
    /// </summary>
    private void SyncTeams()
    {
        syncSpectatorIDs = CopyToUintArray(spectators);
        syncHiderIDs = CopyToUintArray(hiders);
        syncSeekerIDs = CopyToUintArray(seekers);
    }

    private static UintArrayContainer CopyToUintArray(HashSet<uint> collection) 
    {
        var tmp = new UintArrayContainer(collection.Count);
        collection.CopyTo(tmp.value);
        return tmp;
    }

    private void NotifySpectators()
    {
        //Debug.Log($"{this} spectators notified. Spectators count is {spectators.Count}");
        EventBus<OnSpectatorsUpdated>.Raise(this, new OnSpectatorsUpdated { spectators = spectators });
    }

    private void NotifyHiders()
    {
        //Debug.Log($"{this} hiders notified. Hiders count is {hiders.Count}");
        EventBus<OnHidersUpdated>.Raise(this, new OnHidersUpdated { hiders = hiders });
    }

    private void NotifySeekers()
    {
        //Debug.Log($"{this} seekers notified. Seekers count is {seekers.Count}");
        EventBus<OnSeekersUpdated>.Raise(this, new OnSeekersUpdated { seekers = seekers });
    }

    private void NotifyAll()
    {
        NotifySpectators();
        NotifyHiders();
        NotifySeekers();
    }


}
