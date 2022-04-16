using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using System;

public class PlayerEventsDebugger : MonoBehaviour
{
    private void OnEnable()
    {   
        EventBus<OnPropSelected>.Subscribe(PropSelected);
    }

    private void PropSelected(object caller, OnPropSelected propSelected, object target)
    {
        Debug.Log($"{this}. Player {target} selected prop {propSelected.prop}");
    }

    private void OnDisable()
    {
        EventBus<OnPropSelected>.UnSubscribe(PropSelected);
    }
}
