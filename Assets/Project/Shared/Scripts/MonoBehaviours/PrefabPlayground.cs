using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPlayground : MonoBehaviour
{
    [SerializeField]
    private CameraFollowProfile cameraFollowProfile;
    [ContextMenu("Update")]
    private void Setup()
    {
        if (GetComponent<PropPresentationActivation>() == null) gameObject.AddComponent<PropPresentationActivation>();
        var prof = transform.Find("CameraProfile");
        //if (prof != null)
        //{
        //    Destroy(prof.gameObject);
        //}
        //var go = new GameObject("CameraProfile", new System.Type[] { typeof(CameraProfileEvent) });
        //go.transform.SetParent(transform);
        //go.SetActive(false);
        CameraFollowProfileContainer profileContainer;
        if(TryGetComponent<CameraFollowProfileContainer>(out profileContainer))
        {
            DestroyImmediate(profileContainer,true);
        }
        PlayerEventBus eventBus;
        if(TryGetComponent<PlayerEventBus>(out eventBus))
        {
            DestroyImmediate(eventBus,true);
        }
        if(prof != null)
        {
            prof.GetComponent<CameraProfileEvent>().raiseInHierarchy = true;
            prof.GetComponent<CameraProfileEvent>().profile = cameraFollowProfile;
        }
    }
}
