using UnityEngine;
using System.Linq;


namespace GameFlow
{
    [CreateAssetMenu(menuName = "GameState/ParentState")]
    public class ParentState : GameState
    {
        [SerializeField]
        private GameState[] childStates;
        public override void OnEnter(GameManager game)
        {
            foreach(GameState state in childStates)
            {
                state.OnEnter(game);
            }
        }

        public override void OnExit(GameManager game)
        {
            foreach (GameState state in childStates)
            {
                state.OnExit(game);
            }
        }

        public override void OnUpdate(GameManager game)
        {
            foreach (GameState state in childStates)
            {
                state.OnUpdate(game);
            }
        }

    }
}

