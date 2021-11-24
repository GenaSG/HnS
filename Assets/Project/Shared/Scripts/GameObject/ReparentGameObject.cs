using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparentGameObject : MonoBehaviour
{
    [SerializeField]
    private GameObject target,newParent;
    [SerializeField]
    private Vector3 newLocalPosition = Vector3.zero;
    [SerializeField]
    private Vector3 newLocalAngles = Vector3.zero;

    private GameObject originalParent;
    private Vector3 originalLocalPosition = Vector3.zero;
    private Vector3 originalLocalAngles = Vector3.zero;

    private void OnEnable()
    {
        originalParent = target.transform.parent.gameObject;
        originalLocalPosition = target.transform.localPosition;
        originalLocalAngles = target.transform.localEulerAngles;
        target.transform.SetParent(newParent.transform);
        target.transform.localPosition = newLocalPosition;
        target.transform.localEulerAngles = newLocalAngles;
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        target.transform.SetParent(originalParent.transform);
        target.transform.localPosition = originalLocalPosition;
        target.transform.localEulerAngles = originalLocalAngles;
    }
}
