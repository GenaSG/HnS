using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerEventBus))]
public class LookingAt : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float checkDistance = 5f;
    private PlayerEventBus eventBus;
    private GameObject last;


    // Start is called before the first frame update
    void Awake()
    {
        eventBus = GetComponent<PlayerEventBus>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Linecast(transform.position, transform.position + transform.forward * checkDistance,out hit, layerMask))
        {
            if(last != hit.collider.gameObject)
            {
                //Debug.Log("Looking at + " + hit.collider.name);
                eventBus.onPlayerLookingAtChannel.Invoke(this, hit);
                last = hit.collider.gameObject;
            }
            
            
        }
    }
}
