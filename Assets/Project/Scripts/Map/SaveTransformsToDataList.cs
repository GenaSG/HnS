using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveTransformsToDataList : MonoBehaviour
{
    [SerializeField]
    private DataList<Transform> transforms;

    [SerializeField]
    private bool SaveOnAwake = false;

    private void Awake()
    {
        if (!SaveOnAwake) return;
        IEnumerable<Transform> children = transform.Cast<Transform>();
        transforms.AddRange(children);
    }

    public void UpdateList()
    {
        IEnumerable<Transform> children = transform.Cast<Transform>();
        transforms.AddRange(children);
        Debug.Log(transforms.Count);
    }

    public void ClearList()
    {
        transforms.Clear();
    }

    private void OnDestroy()
    {
        transforms.Clear();
    }
}
