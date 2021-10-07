using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerEventBus)), RequireComponent(typeof(CameraSmoothFollow))]
public class CameraFollowProfileSetter : MonoBehaviour
{
    private PlayerEventBus playerEventBus;
    private CameraSmoothFollow follow;
    private void OnEnable()
    {
        playerEventBus = GetComponent<PlayerEventBus>();
        follow = GetComponent<CameraSmoothFollow>();
        playerEventBus.onCameraFollowProfileChanged.AddListener( Channel_OnEvent);

    }

    private void Channel_OnEvent(object caller, CameraFollowProfile profile)
    {
        follow.SetProfile(profile);
    }

    private void OnDisable()
    {
        playerEventBus.onCameraFollowProfileChanged.RemoveListener(Channel_OnEvent);
    }
}
