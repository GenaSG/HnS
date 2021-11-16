using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRegistrator : MonoBehaviour
{
    [SerializeField]
    private DataList<GameObject> registry;
    [SerializeField]
    private GameObject target;
    private void OnEnable()
    {
        if (target == null) target = gameObject;
        registry.Add(target);
    }

    private void OnDisable()
    {
        registry.Remove(target);
    }
}
