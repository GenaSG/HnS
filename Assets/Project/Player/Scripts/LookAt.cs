using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float checkDistance = 5f;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform hitPointTransform;
    [SerializeField]
    private GameObject lookingAt;
    public event Action OnObjectChanged = delegate {};

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Linecast(startPoint.position, startPoint.position + startPoint.forward * checkDistance,out hitInfo, layerMask))
        {
            if (hitPointTransform != null)
            {
                hitPointTransform.gameObject.SetActive(true);
                hitPointTransform.position = hitInfo.point;
            }
            if(hitInfo.collider.gameObject != lookingAt)
            {
                lookingAt = hitInfo.collider.gameObject;
                OnObjectChanged();
            }
        }
        else
        {
            if (hitPointTransform != null) hitPointTransform.gameObject.SetActive(false);
        }
    }
}
