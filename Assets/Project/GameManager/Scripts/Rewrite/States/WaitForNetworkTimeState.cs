using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace GameFlow
{
    [CreateAssetMenu(menuName = "GameState/WaitForNetworkTimeState")]
    public class WaitForNetworkTimeState : GameState
    {
        [SerializeField]
        private GameState nextState;
        [SerializeField]
        private double delay = 1;

        public override void OnEnter(GameManager game)
        {
            
        }

        public override void OnExit(GameManager game)
        {
            
        }

        public override void OnUpdate(GameManager game)
        {
            if (NetworkTime.time > delay) game.SetState(nextState);
        }

    }
}

