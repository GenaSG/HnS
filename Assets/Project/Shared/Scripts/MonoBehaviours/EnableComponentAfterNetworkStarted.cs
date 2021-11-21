using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnableComponentAfterNetworkStarted : NetworkBehaviour
{
    [SerializeField]
    private MonoBehaviour target;

    public override void OnStartClient()
    {
        base.OnStartClient();
        target.enabled = true;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        target.enabled = true;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        target.enabled = false;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        target.enabled = false;
    }
}
