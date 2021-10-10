using UnityEngine;

public class OnCameraFollowProfileChanged : Channel<OnCameraFollowProfileChanged, CameraFollowProfile> { }
public class OnPlayerLookingAtChannel : Channel<OnPlayerLookingAtChannel, RaycastHit> { }

public class PlayerEventBus : MonoBehaviour
{
    public readonly OnCameraFollowProfileChanged onCameraFollowProfileChanged = new OnCameraFollowProfileChanged();
    public readonly OnPlayerLookingAtChannel onPlayerLookingAtChannel = new OnPlayerLookingAtChannel();

    private void Awake()
    {
        onCameraFollowProfileChanged.Init(transform.root);
        onPlayerLookingAtChannel.Init(transform.root);
    }
}
