using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using Mirror;

public class PlayerSpawnEvent : MonoBehaviour
{
    private void Start()
    {
        var netID = GetComponent<NetworkIdentity>();
        EventBus<OnPlayerObjectSpawned>.Raise(this,
            new OnPlayerObjectSpawned
            {
                netID = netID.netId,
                playerObject = gameObject,
                isClient = netID.isClient,
                isServer = netID.isServer,
                isLocalPlayer = netID.isLocalPlayer,
            });
    }

    private void OnDisable()
    {
        var netID = GetComponent<NetworkIdentity>();
        EventBus<OnPlayerObjectDestroyed>.Raise(this,
            new OnPlayerObjectDestroyed
            {
                netID = netID.netId,
                playerObject = gameObject,
                isClient = netID.isClient,
                isServer = netID.isServer,
                isLocalPlayer = netID.isLocalPlayer,
            });
    }
}
