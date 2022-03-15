using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using SimpleEventBus;

namespace GameFlow
{
    public class GameManager : NetworkBehaviour
    {
        [SerializeField]
        private GameState startState;
        [SerializeField]
        private GameState[] states;
        private Dictionary<GameState, uint> inventory = new Dictionary<GameState, uint>();
        private GameState currentState;
        [SyncVar(hook = nameof(OnStateIndexSynced))]
        private uint currentStateIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            for (uint i = 0; i < states.Length; i++)
            {
                inventory.Add(states[i],i);
            }
            if (startState != null) SetState(startState);
        }

        public void SetState(GameState state)
        {
            if (state == null) return;
            if (!inventory.ContainsKey(state)) return;
            if (state == currentState) return;
           
            if (currentState != null) currentState.OnExit(this);
            state.OnEnter(this);
            currentState = state;
            currentStateIndex = inventory[currentState];

            EventBus<OnGameStateChanged>.Raise(this,
                new OnGameStateChanged {
                    gameStateIndex = currentStateIndex,
                    newState = currentState
                });
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState != null) currentState.OnUpdate(this);
        }

        [ClientCallback]
        void OnStateIndexSynced(uint last,uint current)
        {
            SetState(states[current]);
        }
    }
}

