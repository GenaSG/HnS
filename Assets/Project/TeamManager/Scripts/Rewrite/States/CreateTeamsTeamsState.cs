using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateTeamsTeamsState : TeamsManagerState
{

    public override void OnEnter(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers)
    {
        if (spectators.Count == 0) return;
        
        var rand = new System.Random(System.DateTime.Now.Millisecond);
        uint selectedSeekerID = spectators.ToArray()[rand.Next(0,spectators.Count)];
        seekers.Add(selectedSeekerID);
        spectators.Remove(selectedSeekerID);
        hiders.Clear();
        foreach (uint id in spectators)
        {
            hiders.Add(id);
        }
        spectators.Clear();
    }

    public override void OnExit(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers)
    {
       
    }

    public override void OnPlayerConnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID)
    {
        if (!hiders.Contains(playerID)) hiders.Add(playerID);
    }

    public override void OnPlayerDied(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID)
    {
        
    }

    public override void OnPlayerDisconnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID)
    {
        if (spectators.Contains(playerID))
            spectators.Remove(playerID);
        if (hiders.Contains(playerID))
            hiders.Remove(playerID);
        if (seekers.Contains(playerID))
            seekers.Remove(playerID);
    }


}
