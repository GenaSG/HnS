using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using SimpleEventBus;

namespace GameFlow
{
    [CreateAssetMenu(menuName = "GameState/TimedGameState")]
    public class TimedState : GameState
    {
        [SerializeField]
        private GameState nextState;
        [SerializeField]
        private double stateDuration = 15;
        private double stateEnterTime;
        private double stateTime;

        public override void OnEnter(GameManager game)
        {
            stateEnterTime = NetworkTime.time;
            stateTime = 0;
        }

        public override void OnExit(GameManager game)
        {
            
        }

        public override void OnUpdate(GameManager game)
        {
            if (stateTime >= stateDuration)
            {
                game.SetState(nextState);
                return;
            }
         
            stateTime += NetworkTime.time - stateEnterTime;
            EventBus<OnGameStateTimerUpdated>.Raise(this,
                new OnGameStateTimerUpdated
                {
                    stateEnterTime = stateEnterTime,
                    stateTime = stateTime,
                    stateDuration = stateDuration
                });
        }

    }
}

