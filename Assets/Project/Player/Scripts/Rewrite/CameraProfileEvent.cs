using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class CameraProfileEvent : MonoBehaviour
{
    [SerializeField]
    public CameraFollowProfile profile;

    [SerializeField]
    private GameObject eventChannel;
    // Start is called before the first frame update
    void Start()
    {
        EventBus<OnCameraProfileUpdated>.Raise(this, new OnCameraProfileUpdated { profile = this.profile },eventChannel);
    }

}
