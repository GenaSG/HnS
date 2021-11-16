using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState : ScriptableObject
{
    public abstract void OnStateEnter(Game game);
    public abstract void OnStateUpdate(Game game);
    public abstract void OnStateExit(Game game);
}
