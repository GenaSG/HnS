using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName = "Game/TimedGameStateInstance")]
public class TimedGameState : BaseGameState
{
    [SerializeField]
    private double stateTimeOutInSeconds = 30;
    [SerializeField]
    private BaseGameState nextState;

    public override void OnStateEnter(Game game)
    {
        game.StateEnterTime = NetworkTime.time;
        game.StateElapsedTime = 0;
        game.StateTimeLeft = stateTimeOutInSeconds;
    }

    public override void OnStateExit(Game game)
    {
        game.StateEnterTime = NetworkTime.time;
        game.StateElapsedTime = stateTimeOutInSeconds;
        game.StateTimeLeft = 0;
    }

    public override void OnStateUpdate(Game game)
    {
        game.StateElapsedTime = NetworkTime.time - game.StateEnterTime;
        game.StateTimeLeft = stateTimeOutInSeconds - game.StateElapsedTime;
        if (NetworkTime.time >= game.StateEnterTime + stateTimeOutInSeconds) game.SwitchState(nextState);
    }

}
