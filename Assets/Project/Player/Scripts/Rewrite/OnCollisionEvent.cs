using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField]
    private bool raiseInHierarchy;
    [SerializeField]
    private bool trigger;

    private void OnCollisionEnter(Collision collision)
    {
        if (trigger) return;
        EventBus<OnCollisionChanged>.Raise(transform.root.gameObject,
                new OnCollisionChanged { state = CollisionState.OnEnter, collision = collision },
                raiseInHierarchy? transform.root.gameObject : null);
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (trigger) return;
        EventBus<OnCollisionChanged>.Raise(this,
                new OnCollisionChanged { state = CollisionState.OnExit, collision = collision },
                raiseInHierarchy ? transform.root.gameObject : null);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!trigger) return;
        EventBus<OnTriggerChanged>.Raise(this,
                new OnTriggerChanged { state = CollisionState.OnEnter, collider = other },
                raiseInHierarchy ? transform.root.gameObject : null);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!trigger) return;
        EventBus<OnTriggerChanged>.Raise(this,
                new OnTriggerChanged { state = CollisionState.OnExit, collider = other },
                raiseInHierarchy ? transform.root.gameObject : null);
        
    }
}
