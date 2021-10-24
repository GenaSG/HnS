using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(fileName = "GameState", menuName = "Game/GameState")]
public class GameState : ScriptableObject
{
    public List<GameObject> Props => MapController.Props;
    public event Action OnMapGenerated { add { MapController.OnAnyMapGenerated += value; } remove { MapController.OnAnyMapGenerated -= value; } }

    public double GameTime { get { return NetworkTime.time; } }
    public double CurrentStageStartTime { get; }

    public GameStage CurrentGameStage { get { return GameStateController.CurrentGameStage; } }
    public event Action OnGameStageChanged { add { GameStateController.GlobalGameStageChanged += value; } remove { GameStateController.GlobalGameStageChanged -= value; } }

    //public int CurrentStageTimer { get { return GameStateController.CurrentStageTimer; } }
    //public event Action OnCurrentStageTimerUpdated { add { GameStateController.GlobalCurrentStageTimerUpdated += value; } remove { GameStateController.GlobalCurrentStageTimerUpdated -= value; } }

    //public event Action OnGlobalGameEnded { add { GameStateController.GlobalGameEnded += value; } remove { GameStateController.GlobalGameEnded -= value; } }

}
