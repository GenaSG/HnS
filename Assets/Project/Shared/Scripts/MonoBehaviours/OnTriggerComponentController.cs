using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerComponentController : MonoBehaviour
{
    [SerializeField]
    private Behaviour ActivateOnEnter;
    [SerializeField]
    private Behaviour ActivateOnExit;
    [SerializeField]
    private Behaviour DeactivateOnEnter;
    [SerializeField]
    private Behaviour DeactivateOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if(ActivateOnEnter) ActivateOnEnter.enabled = true;
        if(DeactivateOnEnter) DeactivateOnEnter.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(ActivateOnExit) ActivateOnExit.enabled = true;
        if(DeactivateOnExit) DeactivateOnExit.enabled = false;
    }
}
