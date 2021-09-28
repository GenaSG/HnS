using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnCameraProfileChangeChannel))]
public class CameraFollowProfileContainer : MonoBehaviour
{
    public CameraFollowProfile profile;
    private OnCameraProfileChangeChannel channel;

    private void OnEnable()
    {
        channel = GetComponent<OnCameraProfileChangeChannel>();
        channel.Invoke(this, profile);
    }

}
