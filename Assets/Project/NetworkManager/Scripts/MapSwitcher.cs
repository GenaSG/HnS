using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MapSwitcher : MonoBehaviour
{
    [SerializeField]
    private NetworkManager networkManager;
    [SerializeField]
    private Game game;
    [SerializeField]
    private BaseGameState TriggerOnState;

    private void OnEnable()
    {
        game.OnStateChanged += Game_OnStateChanged;   
    }

    private void Game_OnStateChanged()
    {
        if(game.CurrentState == TriggerOnState)
        {
            if (NetworkServer.active) networkManager.ServerChangeScene(networkManager.onlineScene);
        }
    }

    private void OnDisable()
    {
        game.OnStateChanged -= Game_OnStateChanged;
    }
}
