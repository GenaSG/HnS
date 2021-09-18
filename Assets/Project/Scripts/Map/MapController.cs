using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonArchitect;
using DungeonArchitect.Builders.SimpleCity;
using UnityEngine;


[RequireComponent(typeof(GenerateMapWithSeedChannel)),
RequireComponent(typeof(OnMapGenerationDoneChannel)),
RequireComponent(typeof(SimpleCityDungeonConfig)),
RequireComponent(typeof(Dungeon))]
public class MapController : DungeonEventListener
{

    [SerializeField]
    private DataList<Transform> transforms;

    private GenerateMapWithSeedChannel generateMapChannel;
    private OnMapGenerationDoneChannel genereationDoneChannel;
    private PrefabEventListener prefabEventListener;


    private Dungeon dungeon;

    public class PrefabEventListener : DungeonItemSpawnListener
    {
        public DataList<Transform> transforms;

        public override void SetMetadata(GameObject dungeonItem, DungeonNodeSpawnData spawnData)
        {
            base.SetMetadata(dungeonItem, spawnData);
            transforms.Add(dungeonItem.transform);
        }
    }
    #region Initialization
    private void Awake()
    {
        generateMapChannel = GetComponent<GenerateMapWithSeedChannel>();
        genereationDoneChannel = GetComponent<OnMapGenerationDoneChannel>();
        prefabEventListener = gameObject.AddComponent<PrefabEventListener>();
        prefabEventListener.transforms = transforms;
        dungeon = GetComponent<Dungeon>();

    }

    private void OnEnable()
    {
        generateMapChannel.OnEvent += GenerateMapChannel_OnEvent;
    }
    #endregion

    #region Cleanup
    private void OnDisable()
    {
        generateMapChannel.OnEvent -= GenerateMapChannel_OnEvent;
    }

    private void OnDestroy()
    {
        transforms.Clear();
    }
    #endregion

    private void GenerateMapChannel_OnEvent(object caller, uint seed)
    {
        dungeon.Config.Seed = seed;

        dungeon.Build();
    }

    #region DungeonEvents
    public override void OnPreDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPreDungeonBuild(dungeon, model);

        transforms.Clear();

    }

    public override void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPostDungeonBuild(dungeon, model);
        genereationDoneChannel.Invoke(this, true);
    }

    public override void OnDungeonDestroyed(Dungeon dungeon)
    {
        base.OnDungeonDestroyed(dungeon);
        transforms.Clear();
    }

    public override void OnDungeonMarkersEmitted(Dungeon dungeon, DungeonModel model, LevelMarkerList markers)
    {
        base.OnDungeonMarkersEmitted(dungeon, model, markers);

    }

    public override void OnPostDungeonLayoutBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPostDungeonLayoutBuild(dungeon, model);

    }

    public override void OnPreDungeonDestroy(Dungeon dungeon)
    {
        base.OnPreDungeonDestroy(dungeon);

    }
    #endregion
}
