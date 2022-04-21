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
        EventBus<OnLookingAt>.Subscribe(LookingAtObject);
    }

    private void PropSelected(object caller, OnPropSelected propSelected, object target)
    {
        Debug.Log($"{this}. Player {target} selected prop {propSelected.prop}");
    }

    private void LookingAtObject(object caller, OnLookingAt lookingAt,object target){
        Debug.Log($"{this}. Player {target} is looking at  {lookingAt.subject}");
    }

    private void OnDisable()
    {
        EventBus<OnPropSelected>.UnSubscribe(PropSelected);
        EventBus<OnLookingAt>.UnSubscribe(LookingAtObject);
    }
}
