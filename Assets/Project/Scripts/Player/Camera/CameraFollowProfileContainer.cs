using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerEventBus)),DisallowMultipleComponent]
public class CameraFollowProfileContainer : MonoBehaviour
{
    public CameraFollowProfile profile;
    private PlayerEventBus playerEventBus;

    private void OnEnable()
    {
        playerEventBus = GetComponent<PlayerEventBus>();
        playerEventBus.onCameraFollowProfileChanged.Invoke(this, profile);
    }

}
