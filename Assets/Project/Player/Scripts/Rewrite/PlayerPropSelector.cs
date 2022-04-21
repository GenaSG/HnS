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
    [SerializeField]
    private GameObject eventChannel;

    private void OnEnable()
    {
        EventBus<OnLookingAt>.Subscribe(TriggerChanged);
    }

    private void OnDisable()
    {
        EventBus<OnLookingAt>.UnSubscribe(TriggerChanged);
    }

    private void TriggerChanged(object caller, OnLookingAt triggerChanged, object target)
    {
        if (target != (object)eventChannel) return;
//        bool condition = inventory.PropsContains(triggerChanged.subject) && triggerChanged.state == CollisionState.OnEnter;
//        condition = condition && currentProp != triggerChanged.subject;
        bool condition = triggerChanged.subject != null && inventory.PropsContains(triggerChanged.subject);

        if (condition)
        {
            currentProp = triggerChanged.subject;
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
        EventBus<OnPropSelected>.Raise(this,new OnPropSelected { prop = prop }, eventChannel);
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
