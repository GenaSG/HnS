using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFlow {
    public abstract class GameState : ScriptableObject
    {
        public abstract void OnEnter(GameManager game);
        public abstract void OnUpdate(GameManager game);
        public abstract void OnExit(GameManager game);
    }
}

