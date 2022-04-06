using UnityEngine;
using System.Collections;
using SimpleEventBus;
using System;

public class PlayerPropSelector : MonoBehaviour
{
    [SerializeField]
    private MapInventory inventory;
    [SerializeField]
    private GameObject currentProp;

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


}
