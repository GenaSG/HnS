using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField]
    private bool trigger;
    [SerializeField]
    private GameObject eventChannel;

    private void OnCollisionEnter(Collision collision)
    {
        if (trigger) return;
        EventBus<OnCollisionChanged>.Raise(this,
                new OnCollisionChanged { state = CollisionState.OnEnter, collision = collision },
                eventChannel);
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (trigger) return;
        EventBus<OnCollisionChanged>.Raise(this,
                new OnCollisionChanged { state = CollisionState.OnExit, collision = collision },
                eventChannel);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!trigger) return;
        EventBus<OnTriggerChanged>.Raise(this,
                new OnTriggerChanged { state = CollisionState.OnEnter, collider = other },
                eventChannel);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!trigger) return;
        EventBus<OnTriggerChanged>.Raise(this,
                new OnTriggerChanged { state = CollisionState.OnExit, collider = other },
                eventChannel);
        
    }
}
