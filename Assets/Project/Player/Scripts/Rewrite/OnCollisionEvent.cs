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
        if (raiseInHierarchy)
        {
            EventBus<OnCollisionChanged>.Raise(transform.root.gameObject,this,
                new OnCollisionChanged { state = CollisionState.OnEnter, collision = collision});
        }
        else
        {
            EventBus<OnCollisionChanged>.Raise(this,
                new OnCollisionChanged { state = CollisionState.OnEnter, collision = collision });
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (trigger) return;
        if (raiseInHierarchy)
        {
            EventBus<OnCollisionChanged>.Raise(transform.root.gameObject, this,
                new OnCollisionChanged { state = CollisionState.OnExit, collision = collision });
        }
        else
        {
            EventBus<OnCollisionChanged>.Raise(this,
                new OnCollisionChanged { state = CollisionState.OnExit, collision = collision });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!trigger) return;
        if (raiseInHierarchy)
        {
            EventBus<OnTriggerChanged>.Raise(transform.root.gameObject, this,
                new OnTriggerChanged { state = CollisionState.OnEnter, collider = other });
        }
        else
        {
            EventBus<OnTriggerChanged>.Raise(this,
                new OnTriggerChanged { state = CollisionState.OnEnter, collider = other });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!trigger) return;
        if (raiseInHierarchy)
        {
            EventBus<OnTriggerChanged>.Raise(transform.root.gameObject, this,
                new OnTriggerChanged { state = CollisionState.OnExit, collider = other });
        }
        else
        {
            EventBus<OnTriggerChanged>.Raise(this,
                new OnTriggerChanged { state = CollisionState.OnExit, collider = other });
        }
    }
}
