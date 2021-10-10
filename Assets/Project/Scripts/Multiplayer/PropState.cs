using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
[RequireComponent(typeof(PlayerEventBus))]
public class PropState : MultiplayerPlayerControllerState
{
    [SyncVar(hook=nameof(OnObjectIDUpdated))]
    int objectID;
    [SerializeField]
    private string propTag = "Prop";
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
    private PlayerEventBus playerEventBus;
    private Transform observed;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayerPresentation = defaultPlayerPresentation;
        playerEventBus = GetComponent<PlayerEventBus>();
    }

    private void Start()
    {
        if(playerEventBus == null) playerEventBus = GetComponent<PlayerEventBus>();
        playerEventBus.onPlayerLookingAtChannel.AddListener(OnLookingAt);
    }

    private void OnDisable()
    {
        playerEventBus.onPlayerLookingAtChannel.RemoveListener(OnLookingAt);
    }

    private void OnLookingAt(object caller, RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag(propTag)) observed = hit.collider.transform;
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
        if (availablePresentations.Contains(observed))
        {
            objectID = availablePresentations.IndexOf(observed);
            ReplacePresentation(objectID);
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
        foreach(Transform child in currentPlayerPresentation.gameObject.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = gameObject.layer;
        }
    }
}
