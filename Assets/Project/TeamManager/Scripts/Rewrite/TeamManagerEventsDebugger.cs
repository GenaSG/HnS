using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using System;

public class TeamManagerEventsDebugger : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus<OnSpectatorsUpdated>.Subscribe(SpectatorsUpdated);
        EventBus<OnHidersUpdated>.Subscribe(HidersUpdated);
        EventBus<OnSeekersUpdated>.Subscribe(SeekersUpdated);
    }

    private void SeekersUpdated(object caller, OnSeekersUpdated seekersUpdated, object target)
    {
        var ids = "";
        foreach(uint id in seekersUpdated.seekers)
        {
            ids += $"{id} ";
        }
        Debug.Log($"{this} seekers updated {ids}. Called by {caller}.");
    }

    private void HidersUpdated(object caller, OnHidersUpdated hidersUpdated, object target)
    {
        var ids = "";
        foreach (uint id in hidersUpdated.hiders)
        {
            ids += $"{id} ";
        }
        Debug.Log($"{this} hiders updated {ids}. Called by {caller}.");
    }

    private void SpectatorsUpdated(object caller, OnSpectatorsUpdated spectatorsUpdated, object target)
    {
        var ids = "";
        foreach (uint id in spectatorsUpdated.spectators)
        {
            ids += $"{id} ";
        }
        Debug.Log($"{this} spectators updated {ids}. Called by {caller}.");
    }



    private void OnDisable()
    {
        EventBus<OnSpectatorsUpdated>.UnSubscribe(SpectatorsUpdated);
        EventBus<OnHidersUpdated>.UnSubscribe(HidersUpdated);
        EventBus<OnSeekersUpdated>.UnSubscribe(SeekersUpdated);
    }


}
