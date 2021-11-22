using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Team/TeamManager")]
public class TeamManager : ScriptableObject
{
    [SerializeField]
    private int startSeekers = 1;
    //[SerializeField]
    //private DataList<GameObject> players;
    [SerializeField]
    private DataList<uint> playerIDs;
    [SerializeField]
    private DataList<uint> hiderIDs;
    [SerializeField]
    private DataList<uint> seekerIDs;
    //[SerializeField]
    //private DataList<GameObject> hiders;
    //[SerializeField]
    //private DataList<GameObject> seekers;
    public event Action OnTeamsUpdated = delegate { };


    public void UpdateTeams()
    {
        ClearTeams();
        if (playerIDs.Count == 0 || hiderIDs.IsReadOnly || seekerIDs.IsReadOnly) return;
        List<uint> tmpHiders = new List<uint>(playerIDs);
        List<uint> tmpSeekers = new List<uint>();
        //hiders.AddRange(players);

        for (int i = 0; i < startSeekers; i++)
        {
            var player = playerIDs[UnityEngine.Random.Range(0, playerIDs.Count - 1)];
            //if (!hiders.IsReadOnly) hiders.Remove(player);
            //if (!seekers.IsReadOnly) seekers.Add(player);
            tmpHiders.Remove(player);
            tmpSeekers.Add(player);
        }
        hiderIDs.AddRange(tmpHiders);
        seekerIDs.AddRange(tmpSeekers);
        OnTeamsUpdated();

    }

    public void ClearTeams()
    {
        if (!hiderIDs.IsReadOnly) hiderIDs.Clear();
        if (!seekerIDs.IsReadOnly) seekerIDs.Clear();
    }

}
