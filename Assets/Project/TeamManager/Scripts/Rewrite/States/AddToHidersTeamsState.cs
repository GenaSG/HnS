using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddToHidersTeamsState : TeamsManagerState
{
    public override void OnEnter(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers)
    {
        
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

        if (seekers.Count != 0) return;
        if (hiders.Count == 0) return;

        var rand = new System.Random(System.DateTime.Now.Millisecond);
        uint selectedSeekerID = hiders.ToArray()[rand.Next(0, hiders.Count - 1)];
        seekers.Add(selectedSeekerID);
        hiders.Remove(selectedSeekerID);

    }

}
