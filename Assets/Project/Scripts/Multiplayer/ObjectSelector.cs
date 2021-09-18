using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ObjectSelector : NetworkBehaviour
{
    [SyncVar(hook =nameof(OnObjectIDUpdated))]
    ushort objectID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnObjectIDUpdated(ushort lastID, ushort currentID)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

    }
}
