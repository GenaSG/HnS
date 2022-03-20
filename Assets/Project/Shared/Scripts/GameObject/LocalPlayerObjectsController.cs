using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayerObjectsController : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] controlledObjects;
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        foreach(GameObject go in controlledObjects)
        {
            go.SetActive(true);
        }
        
    }

    private void OnDisable()
    {
        foreach (GameObject go in controlledObjects)
        {
            go.SetActive(false);
        }
    }
}
