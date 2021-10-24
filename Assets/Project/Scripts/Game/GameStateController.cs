using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;


public enum GameStage : byte { PreGame, WarmUp, Game, PostGame }

public class GameStateController : NetworkBehaviour
{
    public static GameStage CurrentGameStage { get; private set; }
    public static double CurrentStageStartTime;
    public static event Action GlobalGameStageChanged = delegate { };

    [SyncVar(hook = nameof(OnGameStageSynced))]
    private GameStage syncedGameStage;
    [SyncVar(hook = nameof(OnGameStageStartTimeSynced))]
    private double syncedGameStageStartTime;

    private void OnGameStageSynced(GameStage last, GameStage current)
    {
        CurrentGameStage = current;
        GlobalGameStageChanged();
    }

    private void OnGameStageStartTimeSynced(double last, double current)
    {
        CurrentStageStartTime = current;
    }

    //public static GameStage CurrentGameStage { get; private set; }
    //public static double CurrentStageStartTime;

    //public static event Action GlobalGameStageChanged = delegate { };
    //public event Action OnGameStageChanged = delegate { };

    //public static int CurrentStageTimer { get; private set; }

    //public static event Action GlobalCurrentStageTimerUpdated = delegate { };
    //public event Action OnCurrentStageTimerUpdated = delegate { };

    //public static event Action GlobalGameEnded = delegate { };
    //public event Action OnGameEnded = delegate { };

    //internal abstract class Stage
    //{
    //    [SerializeField]
    //    protected bool isActive;
    //    internal readonly GameStage id;
    //    public abstract void OnEnter(GameStateController controller);

    //    public abstract void OnUpdate(GameStateController controller);

    //    public abstract void OnExit(GameStateController controller);
    //}

    //[Serializable]
    //internal class PreGameStage : Stage
    //{
    //    internal new readonly GameStage id = GameStage.PreGame;
    //    [SerializeField]
    //    private GameState gameState;
    //    private GameStateController controller;

    //    public override void OnEnter(GameStateController controller)
    //    {
    //        gameState.OnMapGenerated += GameState_OnMapGenerated;
    //        this.controller = controller;
    //        isActive = true;
    //    }

    //    private void GameState_OnMapGenerated()
    //    {
    //        controller.SetStage(controller.warmUpStage);
    //    }

    //    public override void OnExit(GameStateController controller)
    //    {
    //        gameState.OnMapGenerated -= GameState_OnMapGenerated;
    //        isActive = false;
    //    }

    //    public override void OnUpdate(GameStateController controller)
    //    {

    //    }
    //}

    //[Serializable]
    //internal class WarmUpStage : Stage
    //{
    //    internal new readonly GameStage id = GameStage.WarmUp;
    //    [SerializeField]
    //    private int warmUpTimeSeconds = 30;
    //    [SerializeField]
    //    private int timer;
    //    private float second;

    //    public override void OnEnter(GameStateController controller)
    //    {
    //        timer = warmUpTimeSeconds;
    //        second = 0f;
    //        isActive = true;
    //    }

    //    public override void OnExit(GameStateController controller)
    //    {
    //        isActive = false;
    //    }

    //    public override void OnUpdate(GameStateController controller)
    //    {
    //        second += Time.deltaTime;
    //        if (second > 1)
    //        {
    //            timer--;
    //            controller.OnCurrentStageTimerUpdated();
    //            GlobalCurrentStageTimerUpdated();
    //            second = 0f;
    //        }

    //        if (timer <= 0) controller.SetStage(controller.inGameStage);
    //    }
    //}

    //[Serializable]
    //internal class InGameStage : Stage
    //{
    //    internal new readonly GameStage id = GameStage.Game;
    //    [SerializeField]
    //    private int gameTimeSeconds = 300;
    //    [SerializeField]
    //    private int timer;
    //    private float second;

    //    public override void OnEnter(GameStateController controller)
    //    {
    //        timer = gameTimeSeconds;
    //        second = 0f;
    //        isActive = true;

    //    }

    //    public override void OnExit(GameStateController controller)
    //    {
    //        isActive = false;
    //    }

    //    public override void OnUpdate(GameStateController controller)
    //    {
    //        second += Time.deltaTime;
    //        if (second > 1)
    //        {
    //            timer--;
    //            controller.OnCurrentStageTimerUpdated();
    //            GlobalCurrentStageTimerUpdated();
    //            second = 0f;
    //        }
    //        if (timer == 0) controller.SetStage(controller.postGameStage);
    //    }
    //}

    //[Serializable]
    //internal class PostGameStage : Stage
    //{
    //    internal new readonly GameStage id = GameStage.PostGame;
    //    [SerializeField]
    //    private int postGameTimeSeconds = 10;
    //    [SerializeField]
    //    private int timer;
    //    private float second;

    //    public override void OnEnter(GameStateController controller)
    //    {
    //        timer = postGameTimeSeconds;
    //        second = 0f;
    //        isActive = true;

    //    }

    //    public override void OnExit(GameStateController controller)
    //    {
    //        isActive = false;
    //    }

    //    public override void OnUpdate(GameStateController controller)
    //    {
    //        second += Time.deltaTime;
    //        if (second > 1)
    //        {
    //            timer--;
    //            controller.OnCurrentStageTimerUpdated();
    //            GlobalCurrentStageTimerUpdated();
    //            second = 0f;
    //        }
    //    }
    //}



    //[SerializeField]
    //private PreGameStage preGameState = new PreGameStage();
    //[SerializeField]
    //private WarmUpStage warmUpStage = new WarmUpStage();
    //[SerializeField]
    //private InGameStage inGameStage = new InGameStage();
    //[SerializeField]
    //private PostGameStage postGameStage = new PostGameStage();

    //private Stage currentStage;

    //[SyncVar(hook = nameof(OnGameStageSynced))]
    //private GameStage syncedGameStage;
    //[SyncVar(hook = nameof(OnStageStartTimeSynced))]
    //private double syncedStageStartTime;

    //internal void SetStage(Stage nextStage)
    //{
    //    if (currentStage != null) currentStage.OnExit(this);
    //    currentStage = nextStage;
    //    currentStage.OnEnter(this);
    //    syncedGameStage = currentStage.id;
    //    CurrentGameStage = syncedGameStage;
    //    OnGameStageChanged();
    //    GlobalGameStageChanged();
    //}

    //public override void OnStartServer()
    //{
    //    SetStage(preGameState);
    //}

    //private void FixedUpdate()
    //{
    //    if (!isServer) return;
    //    currentStage.OnUpdate(this);
    //}

    //private void OnGameStageSynced(GameStage last, GameStage current)
    //{
    //    syncedGameStage = current;
    //    CurrentGameStage = current;
    //}

    //private void OnStageStartTimeSynced(double last, double current)
    //{
    //    syncedStageStartTime = current;
    //    CurrentStageStartTime = current;
    //}
}
