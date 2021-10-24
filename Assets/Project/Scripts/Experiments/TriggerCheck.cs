using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
