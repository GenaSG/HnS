using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using System;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform target;
    public CameraFollowProfile profile;
    private Vector3 lastPosition;
    [SerializeField]
    private GameObject profileEventChannel;


    private void OnEnable()
    {
        EventBus<OnCameraProfileUpdated>.Subscribe(profileEventChannel, CameraProfileUpdated);
    }

    private void CameraProfileUpdated(object caller, OnCameraProfileUpdated profileUpdated)
    {
        this.profile = profileUpdated.profile;
    }

    private void OnDisable()
    {
        EventBus<OnCameraProfileUpdated>.Unsubscribe(profileEventChannel, CameraProfileUpdated);
    }

    public void SetProfile(CameraFollowProfile profile)
    {
        this.profile = profile;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;
        transform.rotation = target.rotation;
        Vector3 targetPosition = target.position + target.right * profile.offset.x + target.up * profile.offset.y + target.forward * profile.offset.z;
        
        if (profile.collideWithGeometry) targetPosition = CheckCollisions(targetPosition);

        if (profile.smoothing)
        {
            transform.position = lastPosition;
            Move(targetPosition);
            lastPosition = transform.position;
        }
        else
        {
            transform.position = targetPosition;
            lastPosition = transform.position;
        }
    }

    void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 1 - Mathf.Exp(-profile.lambda * Time.deltaTime));
    }

    Vector3 CheckCollisions(Vector3 targetPosition)
    {
        RaycastHit hit;
        Vector3 dir = targetPosition - target.position;
        if(Physics.SphereCast(target.position,profile.colliderRadius, dir.normalized,out hit, dir.magnitude, profile.collisionLayers))
        {
            Vector3 toCollisionPoint = hit.point - target.position;

            return target.position + Vector3.Project(toCollisionPoint, dir.normalized) - dir.normalized * profile.colliderRadius;
        }
        return targetPosition;
    }
}
