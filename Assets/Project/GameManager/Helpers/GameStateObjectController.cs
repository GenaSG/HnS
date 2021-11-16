using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateObjectController : MonoBehaviour
{
    [SerializeField]
    private Game game;
    [SerializeField]
    private BaseGameState TriggerOnState;
    private BaseGameState lastState;
    [SerializeField]
    private GameObject ControlledObject;
    // Start is called before the first frame update
    private void OnEnable()
    {
        game.OnStateChanged += Game_OnStateChanged;
        Game_OnStateChanged();
    }

    private void Game_OnStateChanged()
    {
        if(game.CurrentState == TriggerOnState && lastState != TriggerOnState)
        {
            ControlledObject.SetActive(true);
        }
        if (game.CurrentState != TriggerOnState && lastState == TriggerOnState)
        {
            ControlledObject.SetActive(false);
        }
        lastState = game.CurrentState;
    }

    private void OnDisable()
    {
        game.OnStateChanged -= Game_OnStateChanged;
    }
}
