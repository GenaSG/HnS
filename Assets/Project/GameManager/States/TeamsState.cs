using UnityEngine;

[CreateAssetMenu(menuName = "Game/TeamsState")]
public class TeamsState : BaseGameState
{
    private enum TeamsAction { CreateTeams, ClearTeams }
    [SerializeField]
    private TeamManager teamManager;
    [SerializeField]
    private BaseGameState nextState;
    [SerializeField]
    private TeamsAction teamsAction;

    public override void OnStateEnter(Game game)
    {
        if(teamsAction == TeamsAction.CreateTeams)
        {
            teamManager.UpdateTeams();
        }
        else
        {
            teamManager.ClearTeams();
        }
        //Debug.Log("Before switch");
        
    }

    public override void OnStateExit(Game game)
    {
        
    }

    public override void OnStateUpdate(Game game)
    {
        game.SwitchState(nextState);
    }

}
