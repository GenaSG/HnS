using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTeamsTeamsState : TeamsManagerState
{
    public override void OnEnter(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers)
    {
        spectators.UnionWith(hiders);
        spectators.UnionWith(seekers);
        hiders.Clear();
        seekers.Clear();
    }

    public override void OnExit(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers)
    {
        
    }

    public override void OnPlayerConnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID)
    {
       
    }

    public override void OnPlayerDied(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID)
    {
        
    }

    public override void OnPlayerDisconnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID)
    {
        
    }

}
