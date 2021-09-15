using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncPrefabOverNetwork : NetworkBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SyncVar]
    public string syncedPrefabName;

    public override void OnStartServer()
    {
        base.OnStartServer();
        syncedPrefabName = prefab.name;
    }
}
