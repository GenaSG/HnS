using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 angles;
    [SerializeField]
    private Transform target;

    private void OnEnable()
    {
        if (target) target.localEulerAngles = angles;    
    }
}
