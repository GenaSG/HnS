using UnityEngine;

[CreateAssetMenu(fileName = "CameraFollowProfile",menuName = "Camera Follow Profile/Profile")]
public class CameraFollowProfile : ScriptableObject
{
    public bool smoothing;
    public float lambda = 10f;
    public bool collideWithGeometry;
    public LayerMask collisionLayers;
    public float colliderRadius = 0.5f;
    public Vector3 offset;
}
