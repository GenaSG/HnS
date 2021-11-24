using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamGameObjectController : MonoBehaviour
{
    [SerializeField]
    private UIntDataList ids;
    [SerializeField]
    private NetworkIdentity id;
    [SerializeField]
    private GameObject controlledObject;

    private void OnEnable()
    {
        if (ids.Contains(id.netId)) controlledObject.SetActive(true);
    }

    private void OnDisable()
    {
        controlledObject.SetActive(false);
    }
}
