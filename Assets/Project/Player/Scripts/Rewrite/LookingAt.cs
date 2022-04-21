using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;


public class LookingAt : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float checkDistance = 5f;
    [SerializeField]
    private GameObject eventChannel;
    [SerializeField]
    private GameObject observer;
    private GameObject last;



    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        GameObject lookingAt = null;
        if(Physics.Linecast(transform.position, transform.position + transform.forward * checkDistance,out hit, layerMask)) lookingAt = hit.collider.gameObject;

        bool fromNullToObject = last == null && lookingAt != null;
        bool fromObjectToNull = last != null && lookingAt == null;
        bool fromObjectToNewObject = last != null && lookingAt != null && last != lookingAt;

        if(fromNullToObject)
        {
            EventBus<OnLookingAt>.Raise(this, new OnLookingAt() { subject = lookingAt},eventChannel);
            EventBus<OnBeingLookedAt>.Raise(this, new OnBeingLookedAt(){observer = observer}, lookingAt);
            last = lookingAt;
        }
        else if(fromObjectToNull)
        {
            EventBus<OnLookingAt>.Raise(this, new OnLookingAt() { subject = null},eventChannel);
            EventBus<OnBeingLookedAt>.Raise(this, new OnBeingLookedAt(){observer = null}, last);
            last = lookingAt;
        }
        else if(fromObjectToNewObject)
        {
            EventBus<OnLookingAt>.Raise(this, new OnLookingAt() { subject = lookingAt},eventChannel);
            EventBus<OnBeingLookedAt>.Raise(this, new OnBeingLookedAt(){observer = observer}, lookingAt);
            EventBus<OnBeingLookedAt>.Raise(this, new OnBeingLookedAt(){observer = null}, last);
            last = lookingAt;
        }
/*
        if(last != lookingAt)
        {
            EventBus<OnLookingAt>.Raise(this, new OnLookingAt() { subject = lookingAt},eventChannel);
            EventBus<OnBeingLookedAt>.Raise(this, new OnBeingLookedAt(){observer = observer}, lookingAt);
            EventBus<OnBeingLookedAt>.Raise(this, new OnBeingLookedAt(){observer = null}, last);
            last = lookingAt;
        }*/
    }
}
