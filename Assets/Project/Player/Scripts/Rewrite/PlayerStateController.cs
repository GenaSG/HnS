using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;
using GameFlow;
using System;
using Mirror;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField]
    private NetworkIdentity id;
    private enum Team
    {
        None,Spectator,Hider,Seeker
    }
    [SerializeField]
    private GameObject controlledObject;
    [SerializeField]
    private GameState[] targetGameStates;
    [SerializeField]
    private Team[] targetTeams;


    private Team team;
    private GameState gameState;
    private HashSet<GameState> targetGameStatesSet;
    private HashSet<Team> targetTeamsSet;


    private void OnEnable()
    {
        targetGameStatesSet = new HashSet<GameState>(targetGameStates);
        targetTeamsSet = new HashSet<Team>(targetTeams);
        EventBus<OnGameStateChanged>.Subscribe(GameStateChanged);
        EventBus<OnSpectatorsUpdated>.Subscribe(SpectatorsUpdated);
        EventBus<OnHidersUpdated>.Subscribe(HidersUpdated);
        EventBus<OnSeekersUpdated>.Subscribe(SeekersUpdated);

    }

    private void SeekersUpdated(object caller, OnSeekersUpdated seekersUpdated)
    {
        if (seekersUpdated.seekers.Contains(id.netId))
        {
            team = Team.Seeker;
            CheckState();
        } 
    }

    private void HidersUpdated(object caller, OnHidersUpdated hidersUpdated)
    {
        if (hidersUpdated.hiders.Contains(id.netId))
        {
            team = Team.Hider;
            CheckState();
        }
    }

    private void SpectatorsUpdated(object caller, OnSpectatorsUpdated spectatorsUpdated)
    {
        if (spectatorsUpdated.spectators.Contains(id.netId))
        {
            team = Team.Spectator;
            CheckState();
        }
    }

    private void GameStateChanged(object caller, OnGameStateChanged stateChanged)
    {
        gameState = stateChanged.newState;
        CheckState();
    }

    private void CheckState()
    {
        bool condition = targetGameStatesSet.Contains(gameState) && targetTeamsSet.Contains(team);
        bool isActive = controlledObject.activeSelf;
        if(condition && !isActive)
        {
            controlledObject.SetActive(true);
        }
        else if(!condition && isActive)
        {
            controlledObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        EventBus<OnGameStateChanged>.Unsubscribe(GameStateChanged);
        EventBus<OnSpectatorsUpdated>.Unsubscribe(SpectatorsUpdated);
        EventBus<OnHidersUpdated>.Unsubscribe(HidersUpdated);
        EventBus<OnSeekersUpdated>.Unsubscribe(SeekersUpdated);
    }
}
