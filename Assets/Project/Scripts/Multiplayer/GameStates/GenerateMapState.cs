using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonArchitect;
using Mirror;

public class GenerateMapState : StateMachineBehaviour
{
    [SerializeField]
    FindGameObject dungeonGameObject = new FindGameObject();
    private Dungeon dungeon;
    [SerializeField]
    private string MapSeedName = "MapSeed";

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(NetworkServer.active) animator.SetInteger("MapSeed",Random.Range(0,65535));
        dungeon = dungeonGameObject.Object.GetComponent<Dungeon>();
        var seed = animator.GetInteger(MapSeedName);
        if (seed == -1) return;
        dungeon.DestroyDungeon();
        dungeon.Config.Seed = (uint)seed;
        dungeon.Build();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    var seed = animator.GetInteger(MapSeedName);
        
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
