using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameObjectSyncState : NetworkBehaviour
{
    [SerializeField]
    private OnChildrenChanged childrenChanged;
    [SerializeField]
    private GameObject[] syncedObjects;

    public override void OnStartServer()
    {
        base.OnStartServer();
        childrenChanged.OnChildrenChange += ChildrenChanged_OnChildrenChange;
    }

    private void ChildrenChanged_OnChildrenChange()
    {
        
    }

    private void OnDisable()
    {
        childrenChanged.OnChildrenChange -= ChildrenChanged_OnChildrenChange;
    }
}
