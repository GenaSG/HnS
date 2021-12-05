using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerGameObjectController : MonoBehaviour
{
    [SerializeField]
    private GameObject ActivateOnEnter;
    [SerializeField]
    private GameObject ActivateOnExit;
    [SerializeField]
    private GameObject DeactivateOnEnter;
    [SerializeField]
    private GameObject DeactivateOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if(ActivateOnEnter) ActivateOnEnter.SetActive(true);
        if(DeactivateOnEnter) DeactivateOnEnter.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if(ActivateOnExit) ActivateOnExit.SetActive(true);
        if(DeactivateOnExit) DeactivateOnExit.SetActive(false);
    }
}
