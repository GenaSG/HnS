using UnityEngine;

public class OnCameraFollowProfileChanged : Channel<OnCameraFollowProfileChanged, CameraFollowProfile> { }

public class PlayerEventBus : MonoBehaviour
{
    public readonly OnCameraFollowProfileChanged onCameraFollowProfileChanged = new OnCameraFollowProfileChanged();

    private void Awake()
    {
        onCameraFollowProfileChanged.Init(transform.root);
    }
}
