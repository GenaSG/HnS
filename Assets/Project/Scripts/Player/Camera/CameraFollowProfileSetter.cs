using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(OnCameraProfileChangeChannel)), RequireComponent(typeof(CameraSmoothFollow))]
public class CameraFollowProfileSetter : MonoBehaviour
{
    private OnCameraProfileChangeChannel channel;
    private CameraSmoothFollow follow;
    private void OnEnable()
    {
        channel = GetComponent<OnCameraProfileChangeChannel>();
        follow = GetComponent<CameraSmoothFollow>();
        channel.OnEvent += Channel_OnEvent;

    }

    private void Channel_OnEvent(object caller, CameraFollowProfile profile)
    {
        follow.SetProfile(profile);
    }

    private void OnDisable()
    {
        channel.OnEvent -= Channel_OnEvent;
    }
}
