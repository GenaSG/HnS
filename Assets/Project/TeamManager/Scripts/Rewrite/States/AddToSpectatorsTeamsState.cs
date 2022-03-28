using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpectatorsTeamsState : TeamsManagerState
{
    public override void OnEnter(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers)
    {

    }

    public override void OnExit(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers)
    {

    }

    public override void OnPlayerConnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID)
    {
        if(!spectators.Contains(playerID)) spectators.Add(playerID);
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
