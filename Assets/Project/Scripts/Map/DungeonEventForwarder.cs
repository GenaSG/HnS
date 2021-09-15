using System.Collections;
using System.Collections.Generic;
using DungeonArchitect;
using UnityEngine;

public class DungeonEventForwarder : DungeonEventListener
{
    [SerializeField]
    OnMapGenerationDoneChannel OnPostDungeonBuildChannel;
    private void Awake()
    {
        OnPostDungeonBuildChannel = GetComponent<OnMapGenerationDoneChannel>();
    }
    public override void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPostDungeonBuild(dungeon, model);
        OnPostDungeonBuildChannel.Invoke((object)this, true);
    }
}
