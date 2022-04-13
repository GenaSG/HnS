using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using System;

public class PlayerPropSwitcher : MonoBehaviour
{
    //[SerializeField]
    //private MapInventory inventory;

    private void OnEnable()
    {
        EventBus<OnPropSelected>.Subscribe(transform.root.gameObject, PropSelected);
    }

    private void PropSelected(object caller, OnPropSelected selected)
    {
        Instantiate(selected.prop, transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        EventBus<OnPropSelected>.Unsubscribe(transform.root.gameObject, PropSelected);
    }
}
