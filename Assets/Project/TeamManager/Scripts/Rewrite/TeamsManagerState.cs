using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TeamsManagerState : MonoBehaviour
{
    public abstract void OnEnter(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers);
    public abstract void OnExit(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers);

    public abstract void OnPlayerConnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID);
    public abstract void OnPlayerDisconnected(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID);
    public abstract void OnPlayerDied(HashSet<uint> spectators, HashSet<uint> hiders, HashSet<uint> seekers, uint playerID);
}
