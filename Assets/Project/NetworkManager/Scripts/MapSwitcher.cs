using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using GameFlow;
using SimpleEventBus;
using System;

public class MapSwitcher : MonoBehaviour
{
    [SerializeField]
    private NetworkManager networkManager;
    //[SerializeField]
    //private Game game;
    //[SerializeField]
    //private BaseGameState TriggerOnState;
    [SerializeField]
    private GameState triggerOnState;

    private void OnEnable()
    {
        //game.OnStateChanged += Game_OnStateChanged;
        EventBus<OnGameStateChanged>.Subscribe(GameStateChanged);
    }

    private void GameStateChanged(object caller, OnGameStateChanged stateChanged, object target)
    {
        if (stateChanged.newState == triggerOnState)
        {
            if (NetworkServer.active) networkManager.ServerChangeScene(networkManager.onlineScene);
        }
    }

    private void Game_OnStateChanged()
    {
        //if(game.CurrentState == TriggerOnState)
        //{
            //if (NetworkServer.active) networkManager.ServerChangeScene(networkManager.onlineScene);
        //}
    }

    private void OnDisable()
    {
        //game.OnStateChanged -= Game_OnStateChanged;
        EventBus<OnGameStateChanged>.UnSubscribe(GameStateChanged);
    }
}
