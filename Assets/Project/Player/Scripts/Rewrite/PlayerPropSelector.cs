using UnityEngine;
using System.Collections;
using SimpleEventBus;
using System;
using Mirror;

public class PlayerPropSelector : NetworkBehaviour
{
    [SerializeField]
    private MapInventory inventory;
    [SerializeField]
    private GameObject currentProp;
    [SyncVar(hook = nameof(SyncPropID))]
    private uint syncPropID;

    private void OnEnable()
    {
        EventBus<OnTriggerChanged>.Subscribe(transform.root.gameObject, TriggerChanged);
    }

    private void OnDisable()
    {
        EventBus<OnTriggerChanged>.Unsubscribe(transform.root.gameObject, TriggerChanged);
    }

    private void TriggerChanged(object caller, OnTriggerChanged triggerChanged)
    {
        bool condition = inventory.PropsContains(triggerChanged.collider.gameObject) && triggerChanged.state == CollisionState.OnEnter;
        condition = condition && currentProp != triggerChanged.collider.gameObject;

        if (condition)
        {
            currentProp = triggerChanged.collider.gameObject;
        }
        else
        {
            currentProp = null;
        }
    }

    public void SelectCurrentProp()
    {
        if (currentProp == null) return;
        if (!isLocalPlayer) return;

        if (isServer)
        {
            syncPropID = inventory.GetIndexForProp(currentProp);
        }
        else
        {
            CmdSyncPropID(inventory.GetIndexForProp(currentProp));
        }
        Raise(currentProp);
        
    }
    

    private void Raise(GameObject prop)
    {
        EventBus<OnPropSelected>.Raise(transform.root.gameObject, this, new OnPropSelected { prop = currentProp });
    }

    [Command]//Called only on server
    private void CmdSyncPropID(uint id)
    {
        if (!isServer) return;
        if (isLocalPlayer) return;
        if (!inventory.PropsContainsIndex(id)) return;
        currentProp = inventory.GetPropForIndex(id);
        syncPropID = id;
        Raise(currentProp);
    }

    [ClientCallback]//Called only on non-owner client
    private void SyncPropID(uint old, uint current)
    {
        if (isLocalPlayer) return;
        currentProp = inventory.GetPropForIndex(current);
        Raise(currentProp);
    }

    
}
