using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PropsSync : NetworkBehaviour
{
    [SerializeField]
    private Vector3 offset = Vector3.zero;
    [SerializeField]
    private LookAt lookAt;
    [SerializeField]
    private DataList<GameObject> props;
    [SerializeField]
    private GameObject defaultPlayerPresentation;
    private GameObject currentPlayerPresentation;
    [SerializeField]
    private GameObject lookingAtObject;
    [SyncVar(hook = nameof(OnPropIdSynced))]
    private int syncedPropId;
    private int lastPorpId;

    private void OnEnable()
    {
        lookAt.OnObjectChanged += LookAt_OnObjectChanged;
        currentPlayerPresentation = defaultPlayerPresentation;
    }

    private void LookAt_OnObjectChanged()
    {
        lookingAtObject = lookAt.LookingAt;
    }

    private void OnDisable()
    {
        lookAt.OnObjectChanged -= LookAt_OnObjectChanged;
    }

    
    public void SelectCurrentProp()
    {
        if(!isLocalPlayer) return;
        CmdSelectCurrentProp();
    }

    [Command]
    private void CmdSelectCurrentProp()
    {
        if (props.Contains(lookingAtObject))
        {
            syncedPropId = props.IndexOf(lookingAtObject);
           
            ReplacePresentation(syncedPropId);
        }
    }


    private void ReplacePresentation(int presentationId)
    {
        Transform r = defaultPlayerPresentation.transform.parent;


        //if (currentPlayerPresentation != null && currentPlayerPresentation != defaultPlayerPresentation)
        //{
        //    Destroy(currentPlayerPresentation.gameObject);
        //}
        //else if(currentPlayerPresentation != null)
        //{
        //    currentPlayerPresentation.gameObject.SetActive(false);
        //}
        if(currentPlayerPresentation != null)
        {
            if(currentPlayerPresentation == defaultPlayerPresentation)
            {
                currentPlayerPresentation.gameObject.SetActive(false);
            }
            else
            {
                Destroy(currentPlayerPresentation);
            }
        }


        Vector3 position = new Vector3(r.position.x, r.position.y, r.position.z) + offset;
        currentPlayerPresentation = Instantiate<GameObject>(props[presentationId], position, r.rotation, r);
        currentPlayerPresentation.layer = r.gameObject.layer;
        foreach (Transform child in currentPlayerPresentation.gameObject.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = gameObject.layer;
        }
    }

    //[ClientCallback]
    private void OnPropIdSynced(int last,int current)
    {
        syncedPropId = current;
        ReplacePresentation(syncedPropId);
    }

   
}
