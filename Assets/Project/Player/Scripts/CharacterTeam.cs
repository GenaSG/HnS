using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class CharacterTeam : NetworkBehaviour
{
    [SerializeField]
    private List<Team> teams = new List<Team>();
    [SerializeField]
    private Team currentTeam;
    [SyncVar(hook = nameof(OnTeamIDSynced))]
    private int syncedTeamID = -1;
    public Team CurrentTeam { get { return currentTeam; } }
    public event Action OnTeamChanged = delegate {};

    public void SetTeam(Team team)
    {
        if (!isServer) return;
        IntSetTeam(team);
    }

    void IntSetTeam(Team team)
    {
        if (!teams.Contains(team)) return;
        syncedTeamID = teams.IndexOf(team);
        currentTeam = team;
        OnTeamChanged();
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    IntSetTeam(currentTeam);
    //}
    [ClientCallback]
    void OnTeamIDSynced(int old,int current)
    {
        if(current >= 0 && current < teams.Count) IntSetTeam(teams[current]);
    }
}
