using System.Collections;
using System.Collections.Generic;
using DungeonArchitect;
using UnityEngine;

[RequireComponent(typeof(GenerateMapWithSeedChannel)), RequireComponent(typeof(Dungeon))]
public class GenerateMapWithSeed : MonoBehaviour
{
    private GenerateMapWithSeedChannel channel;
    private Dungeon dungeon;

    private void OnEnable()
    {
        dungeon = GetComponent<Dungeon>();
        channel = GetComponent<GenerateMapWithSeedChannel>();
        channel.OnEvent += Channel_OnEvent;
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
