using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class CameraProfileEvent : MonoBehaviour
{
    [SerializeField]
    private CameraFollowProfile profile;
    [SerializeField]
    private bool raiseInHierarchy;
    // Start is called before the first frame update
    void Start()
    {
        if (raiseInHierarchy)
        {
            EventBus<OnCameraProfileUpdated>.Raise(transform.root.gameObject, this,
                new OnCameraProfileUpdated { profile = this.profile });
        }
        else
        {
            EventBus<OnCameraProfileUpdated>.Raise(this,
                new OnCameraProfileUpdated { profile = this.profile });
        }
        
    }

}
