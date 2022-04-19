using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class CameraProfileEvent : MonoBehaviour
{
    [SerializeField]
    public CameraFollowProfile profile;
    [SerializeField]
    public bool raiseInHierarchy;
    // Start is called before the first frame update
    void Start()
    {
        EventBus<OnCameraProfileUpdated>.Raise(transform.root.gameObject,
            new OnCameraProfileUpdated { profile = this.profile },
            raiseInHierarchy ? transform.root.gameObject : null);
    }

}
