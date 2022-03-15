using UnityEngine;

namespace GameFlow
{
    [CreateAssetMenu(menuName = "GameState/MarkerState")]
    public class MarkerState : GameState
    {
        [SerializeField]
        private GameState nextState;
        public override void OnEnter(GameManager game)
        {
            
        }

        public override void OnExit(GameManager game)
        {
           
        }

        public override void OnUpdate(GameManager game)
        {
            game.SetState(nextState);
        }

    }
}

