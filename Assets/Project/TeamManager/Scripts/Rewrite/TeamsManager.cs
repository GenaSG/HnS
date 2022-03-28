using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using SimpleEventBus;
using System;
using System.Linq;

public class TeamsManager : NetworkBehaviour
{
    public struct UintArrayContainer
    {
        public uint[] value;
        public UintArrayContainer(int size)
        {
            value = new uint[size];
        }

        public UintArrayContainer(uint[] value)
        {
            this.value = value;
        }
    }

    //public class NullTeamsState : TeamsManagerState
    //{
    //    public override void OnEnter(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers){}
    //    public override void OnExit(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers){}
    //    public override void OnPlayerConnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID){}
    //    public override void OnPlayerDied(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID){}
    //    public override void OnPlayerDisconnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID){}
    //}

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
    [SerializeField]
    private TeamsManagerState state;

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

    private void NotifyAll()
    {
        NotifySpectators();
        NotifyHiders();
        NotifySeekers();
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

    private void OnEnable()
    {
        //state = gameObject.AddComponent<NullTeamsState>();
        EventBus<OnPlayerObjectSpawned>.Subscribe(PlayerObjectSpawned);
        EventBus<OnPlayerObjectDestroyed>.Subscribe(PlayerObjectDestroyed);
        EventBus<OnPlayerDied>.Subscribe(PlayerDied);
    }

    private void OnDisable()
    {
        EventBus<OnPlayerObjectSpawned>.Unsubscribe(PlayerObjectSpawned);
        EventBus<OnPlayerObjectDestroyed>.Unsubscribe(PlayerObjectDestroyed);
        EventBus<OnPlayerDied>.Unsubscribe(PlayerDied);
    }

    private void PlayerObjectSpawned(object caller, OnPlayerObjectSpawned objectSpawned)
    {
        if (!isServer) return;
        state.OnPlayerConnected(spectators,hiders,seekers,objectSpawned.netID);
        SyncTeams();
        NotifyAll();
    }

    private void PlayerObjectDestroyed(object caller, OnPlayerObjectDestroyed objectDestroyed)
    {
        if (!isServer) return;
        state.OnPlayerDisconnected(spectators, hiders, seekers, objectDestroyed.netID);
        SyncTeams();
        NotifyAll();
    }


    private void PlayerDied(object caller, OnPlayerDied playerDied)
    {
        if (!isServer) return;
        state.OnPlayerDied(spectators, hiders, seekers, playerDied.netID);
        SyncTeams();
        NotifyAll();
    }

    public void SetState(TeamsManagerState newState)
    {
        if (!isServer) return;
        if (newState == null) return;
        if (newState == state) return;
        state.OnExit(spectators, hiders, seekers);
        newState.OnEnter(spectators, hiders, seekers);
        state = newState;
        Debug.Log($"{this}. Switched state to state {state}");
        SyncTeams();
        NotifyAll();
    }

    private void SyncTeams()
    {
        syncSpectatorIDs = new UintArrayContainer(spectators.ToArray());
        syncHiderIDs = new UintArrayContainer(hiders.ToArray());
        syncSeekerIDs = new UintArrayContainer(seekers.ToArray());
    }
}
