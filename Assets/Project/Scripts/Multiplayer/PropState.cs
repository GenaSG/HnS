using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PropState : MultiplayerPlayerControllerState
{
    [SyncVar(hook=nameof(OnObjectIDUpdated))]
    int objectID;
    [SerializeField]
    private LayerMask objectSelectorLayers;
    [SerializeField]
    private float checkDistance = 2f;
    [SerializeField]
    private Transform checkOrigin;
    [SerializeField]
    private Transform defaultPlayerPresentation;
    private Transform currentPlayerPresentation;
    [SerializeField]
    private Vector3 offset = Vector3.zero;
    [SerializeField]
    private DataList<Transform> availablePresentations;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayerPresentation = defaultPlayerPresentation;
    }

    void OnObjectIDUpdated(int lastID, int currentID)
    {
        ReplacePresentation(currentID);
    }

    public override void StrategyUpdate()
    {
        if (!isLocalPlayer) return;
        if (Input.GetButtonDown("Fire1"))
        {
            CmdCheckForObject();
        }
    }

    [Command]
    private void CmdCheckForObject()
    {
        RaycastHit hit;
        Vector3 startPos = checkOrigin.position;
        Vector3 endPosition = startPos + checkOrigin.forward * checkDistance;
        if(Physics.Linecast(startPos,endPosition,out hit, objectSelectorLayers))
        {
            if (availablePresentations.Contains(hit.transform))
            {
                objectID = availablePresentations.IndexOf(hit.transform);
                ReplacePresentation(objectID);
            }
        }
    }

    private void ReplacePresentation(int presentationId)
    {
        Transform r = defaultPlayerPresentation.parent;
        

        if(currentPlayerPresentation != defaultPlayerPresentation)
        {
            Destroy(currentPlayerPresentation.gameObject);
        }
        else
        {
            currentPlayerPresentation.gameObject.SetActive(false);
        }
        Vector3 position = new Vector3(r.position.x, r.position.y,r.position.z) + offset;
        currentPlayerPresentation = Instantiate<Transform>(availablePresentations[presentationId], position, r.rotation, r);
        currentPlayerPresentation.gameObject.layer = gameObject.layer;
    }
}
