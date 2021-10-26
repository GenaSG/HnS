using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using DungeonArchitect;

public class GenerateMap : NetworkBehaviour, IStateCallbackcReceiver
{
    [SerializeField]
    FindGameObject dungeonGameObject = new FindGameObject();
    [SyncVar(hook = nameof(OnSeedReceived))]
    uint seed = 0;
    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isServer) return;
        seed = (uint)(Random.Range(0, 65535));
        GenerateWithSeed(seed);
        animator.GetComponent<NetworkAnimator>().SetTrigger("NextState");
    }

    private void GenerateWithSeed(uint seed)
    {
        var dungeon = dungeonGameObject.Object.GetComponent<Dungeon>();
        dungeon.DestroyDungeon();
        dungeon.Config.Seed = (uint)seed;
        dungeon.Build();
    }

    [ClientCallback]
    private void OnSeedReceived(uint old, uint current)
    {
        if (isServer) return;
        GenerateWithSeed(current);
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
