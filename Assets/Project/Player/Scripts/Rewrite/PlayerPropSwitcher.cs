using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using System;

public class PlayerPropSwitcher : MonoBehaviour
{

    [SerializeField]
    private GameObject defaultPlayerPresentation;
    [SerializeField]
    private GameObject playerPresentationAnchor;
    private GameObject currentPresentation;


    private void OnEnable()
    {
        currentPresentation = defaultPlayerPresentation;
        EventBus<OnPropSelected>.Subscribe(PropSelected);
    }

    private void PropSelected(object caller, OnPropSelected selected, object target)
    {
        if (target != (object)transform.root.gameObject) return;
        bool switched = SetCurrentPresentation(selected.prop);
        bool currentIsDefault = currentPresentation == defaultPlayerPresentation;
        bool switchedToNew = !currentIsDefault && switched;
        bool switchedToDefault = currentPresentation && switched;

        if (switchedToNew)
        {
            
        }
        else if(switchedToDefault)
        {

        }
        if(switched)
            EventBus<OnSelectedBy>.Raise(this, new OnSelectedBy
            {
                user = transform.root.gameObject
            }, currentPresentation);
    }

    private bool SetCurrentPresentation(GameObject presentation)
    {
        bool currentIsDefault = currentPresentation == defaultPlayerPresentation;
        bool presentationChanged = presentation != currentPresentation;
        bool presentationIsDefault = presentation == defaultPlayerPresentation;
        bool switchFromDefaultToNew = currentIsDefault && presentationChanged && !presentationIsDefault;
        bool switchToDefault = !currentIsDefault && presentationChanged && presentationIsDefault;
        bool switchBetweenProps = !currentIsDefault && presentationChanged && !presentationIsDefault;

        if (switchFromDefaultToNew)
        {
            currentPresentation.SetActive(false);
            currentPresentation = InstantiateAndAttach(presentation,playerPresentationAnchor);
            SetLayerRecursevly(currentPresentation, playerPresentationAnchor.layer);
            return true;
        }
        else if (switchToDefault)
        {
            Destroy(currentPresentation);
            currentPresentation = defaultPlayerPresentation;
            currentPresentation.SetActive(true);
            return true;
        }
        else if (switchBetweenProps)
        {
            Destroy(currentPresentation);
            currentPresentation = InstantiateAndAttach(presentation, playerPresentationAnchor);
            SetLayerRecursevly(currentPresentation, playerPresentationAnchor.layer);
            return true;
        }
        return false;
    }

    private static GameObject InstantiateAndAttach(GameObject what,GameObject to)
    {
        return Instantiate(what,
                to.transform.position,
                to.transform.rotation,
                to.transform) as GameObject;
    }

    private static void SetLayerRecursevly(GameObject go, int layers)
    {
        go.layer = layers;
        var parentTransform = go.transform;
        foreach(Transform child in parentTransform)
        {
            child.gameObject.layer = layers;
        }
    }

    private void OnDisable()
    {
        EventBus<OnPropSelected>.UnSubscribe(PropSelected);
    }
}
