using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using System;

public class PropPresentationActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objetsToActivate;
    [SerializeField]
    private GameObject[] objectsToDeactivate;


    private void OnEnable()
    {
        EventBus<OnSelectedBy>.Subscribe(SelectedBy);
    }

    private void SelectedBy(object caller, OnSelectedBy selectedBy, object target)
    {
        if (target != (object)gameObject) return;
        foreach(GameObject o in objetsToActivate)
        {
            o.SetActive(true);
        }

        foreach (GameObject o in objectsToDeactivate)
        {
            o.SetActive(false);
        }
    }

    private void OnDisable()
    {
        EventBus<OnSelectedBy>.UnSubscribe(SelectedBy);
    }

}
