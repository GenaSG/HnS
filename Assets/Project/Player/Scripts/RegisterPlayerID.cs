using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RegisterPlayerID : NetworkBehaviour
{
    [SerializeField]
    private DataList<uint> ids;
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        if (!ids.Contains(netId)) ids.Add(netId);
    }

    private void OnDisable()
    {
        if (ids.Contains(netId) && !ids.IsReadOnly) ids.Remove(netId);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        if (ids.Contains(netId)) ids.Remove(netId);
    }
}
