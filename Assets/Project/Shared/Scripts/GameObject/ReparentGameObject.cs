using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparentGameObject : MonoBehaviour
{
    [SerializeField]
    private GameObject target,newParent;
    private GameObject originalParent;

    private void OnEnable()
    {
        originalParent = target.transform.parent.gameObject;
        target.transform.SetParent(newParent.transform);
    }

    private void OnDisable()
    {
        target.transform.SetParent(originalParent.transform);

    }
}
