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
        ActivateOnEnter.SetActive(true);
        DeactivateOnEnter.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        ActivateOnExit.SetActive(true);
        DeactivateOnExit.SetActive(false);
    }
}
