using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using Mirror;
using System;
using System.Linq;

public class TeamsGameObjectSwitcher : MonoBehaviour
{
    [SerializeField]
    private NetworkIdentity id;
    [SerializeField]
    private GameObject[] spectatorObjects;
    [SerializeField]
    private GameObject[] hiderObjects;
    [SerializeField]
    private GameObject[] seekerObjects;


    private void OnEnable()
    {
        EventBus<OnSpectatorsUpdated>.Subscribe(SpectatorsUpdated);
        EventBus<OnHidersUpdated>.Subscribe(HidersUpdates);
        EventBus<OnSeekersUpdated>.Subscribe(SeekersUpdated);
    }

    private void SeekersUpdated(object caller, OnSeekersUpdated seekersUpdated, object target)
    {
        bool enableObject = seekersUpdated.seekers.Contains(id.netId);
        foreach (GameObject o in seekerObjects)
        {
            o.SetActive(enableObject);
        }
    }

    private void HidersUpdates(object caller, OnHidersUpdated hidersUpdated, object target)
    {
        bool enableObject = hidersUpdated.hiders.Contains(id.netId);
        foreach (GameObject o in hiderObjects)
        {
            o.SetActive(enableObject);
        }
    }

    private void SpectatorsUpdated(object caller, OnSpectatorsUpdated spectatorsUpdated, object target)
    {
        bool enableObject = spectatorsUpdated.spectators.Contains(id.netId);
        foreach (GameObject o in spectatorObjects)
        {
            o.SetActive(enableObject);
        }
    }

    private void OnDisable()
    {
        EventBus<OnSpectatorsUpdated>.UnSubscribe(SpectatorsUpdated);
        EventBus<OnHidersUpdated>.UnSubscribe(HidersUpdates);
        EventBus<OnSeekersUpdated>.UnSubscribe(SeekersUpdated);
    }
}
