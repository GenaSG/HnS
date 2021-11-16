using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private Game game;

    [SyncVar(hook = nameof(OnStateStartTimeSynced))]
    private double syncedStateStartTime;

    [SyncVar(hook = nameof(OnStateIDSynced))]
    private int syncedStateID;

    public override void OnStartServer()
    {
        base.OnStartServer();
        game.OnStateChanged += Game_OnStateChanged;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        game.OnStateChanged -= Game_OnStateChanged;
    }

    private void Game_OnStateChanged()
    {
        syncedStateID = game.GetCurrentStateID();
    }

    private void Start()
    {
        game.Start();
    }

    private void Update()
    {
        game.Update();
    }

    private void OnDestroy()
    {
        game.Stop();
    }

    [ClientCallback]
    private void OnStateStartTimeSynced(double last, double current)
    {
        game.StateEnterTime = current;
    }

    [ClientCallback]
    private void OnStateIDSynced(int last,int current)
    {
        game.SwitchState(current);
    }
}
