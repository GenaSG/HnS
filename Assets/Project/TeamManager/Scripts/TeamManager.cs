using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Team/TeamManager")]
public class TeamManager : ScriptableObject
{
    [SerializeField]
    private int startSeekers = 1;
    [SerializeField]
    private DataList<GameObject> players;
    [SerializeField]
    private DataList<GameObject> hiders;
    [SerializeField]
    private DataList<GameObject> seekers;
    public event Action OnTeamsUpdated = delegate { };


    public void UpdateTeams()
    {
        ClearTeams();
        if (players.Count == 0 || hiders.IsReadOnly || seekers.IsReadOnly) return;
        List<GameObject> tmpHiders = new List<GameObject>(players);
        List<GameObject> tmpSeekers = new List<GameObject>();
        //hiders.AddRange(players);

        for (int i = 0; i < startSeekers; i++)
        {
            var player = players[UnityEngine.Random.Range(0, players.Count - 1)];
            //if (!hiders.IsReadOnly) hiders.Remove(player);
            //if (!seekers.IsReadOnly) seekers.Add(player);
            tmpHiders.Remove(player);
            tmpSeekers.Add(player);
        }
        hiders.AddRange(tmpHiders);
        seekers.AddRange(tmpSeekers);
        OnTeamsUpdated();

    }

    public void ClearTeams()
    {
        if (!hiders.IsReadOnly) hiders.Clear();
        if (!seekers.IsReadOnly) seekers.Clear();
    }

}
