using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayerObjectsController : NetworkBehaviour
{
    [SerializeField]
    private GameObject controlledObject;
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        controlledObject.SetActive(true);
    }

    private void OnDisable()
    {
        controlledObject.SetActive(false);
    }
}
