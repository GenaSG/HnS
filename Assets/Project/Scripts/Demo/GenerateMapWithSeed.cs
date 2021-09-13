using System.Collections;
using System.Collections.Generic;
using DungeonArchitect;
using UnityEngine;

[RequireComponent(typeof(GenerateMapWithSeedChannel)), RequireComponent(typeof(Dungeon)), RequireComponent(typeof(OnMapGenerationDoneChannel))]
public class GenerateMapWithSeed : DungeonEventListener
{
    private GenerateMapWithSeedChannel channel;
    private OnMapGenerationDoneChannel onDoneChannel;
    private Dungeon dungeon;


    private void OnEnable()
    {
        dungeon = GetComponent<Dungeon>();
        channel = GetComponent<GenerateMapWithSeedChannel>();
        onDoneChannel = GetComponent<OnMapGenerationDoneChannel>();
        channel.OnEvent += Channel_OnEvent;

    }

    public override void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPostDungeonBuild(dungeon, model);
        onDoneChannel.Invoke((object)this, true);
    }

    private void Channel_OnEvent(object caller, uint seed)
    {
        dungeon.Config.Seed = seed;
        dungeon.RequestRebuild();
    }

    private void OnDisable()
    {
        if (channel == null) return;
        channel.OnEvent -= Channel_OnEvent;
    }
}
