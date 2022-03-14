using System;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Builders.SimpleCity;
using UnityEngine;
using SimpleEventBus;


[RequireComponent(typeof(SimpleCityDungeonConfig)),
RequireComponent(typeof(Dungeon))]
public class MapController : DungeonEventListener
{
    //public static event Action OnAnyMapGenerated = delegate { };
    //public event Action OnMapGenerated = delegate { };
    [SerializeField]
    private List<GameObject> Props = new List<GameObject>();
    [SerializeField]
    private List<GameObject> MapObjects = new List<GameObject>();
    
    [SerializeField]
    private string PropsTags = "Props";
    
    private PrefabEventListener prefabEventListener;


    private Dungeon dungeon;

    public class PrefabEventListener : DungeonItemSpawnListener
    {
        public List<GameObject> Props;
        public List<GameObject> MapObjects;
        public string PropsTags = "Props";
        public override void SetMetadata(GameObject dungeonItem, DungeonNodeSpawnData spawnData)
        {
            base.SetMetadata(dungeonItem, spawnData);
            
            if (dungeonItem.CompareTag(PropsTags))
            {
                Props.Add(dungeonItem);
            }
            else
            {
                MapObjects.Add(dungeonItem);
            }

        }
    }
    #region Initialization
    private void Awake()
    {
       
        prefabEventListener = gameObject.AddComponent<PrefabEventListener>();
        prefabEventListener.Props = Props;
        prefabEventListener.MapObjects = MapObjects;
        prefabEventListener.PropsTags = PropsTags;
        dungeon = GetComponent<Dungeon>();
    }

    private void OnEnable()
    {
        EventBus<OnMapSeedGenerated>.Subscribe(OnBuildMap);
    }

    #endregion

    #region Cleanup
    private void OnDisable()
    {
        EventBus<OnMapSeedGenerated>.Unsubscribe(OnBuildMap);
    }

    private void OnDestroy()
    {
        Props.Clear();
        MapObjects.Clear();
    }
    #endregion

    private void OnBuildMap(object caller, OnMapSeedGenerated seed)
    {
        Debug.Log("On build map");
        BuildMap(seed.seed);
    }

    public void BuildMap(uint seed)
    {
        dungeon.Config.Seed = seed;
        dungeon.Build();
    }

    public void BuildMap()
    {
        dungeon.Build();
    }

    #region DungeonEvents
    public override void OnPreDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPreDungeonBuild(dungeon, model);

        
        Props.Clear();
        MapObjects.Clear();

    }

    public override void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPostDungeonBuild(dungeon, model);
        EventBus<OnMapGenerated>.Raise(this,
            new OnMapGenerated { props = Props.ToArray(), levelGeometry = MapObjects.ToArray() });
        //OnAnyMapGenerated();
        //OnMapGenerated();
    }

    public override void OnDungeonDestroyed(Dungeon dungeon)
    {
        base.OnDungeonDestroyed(dungeon);
        Props.Clear();
        MapObjects.Clear();
        EventBus<OnMapCleared>.Raise(this, new OnMapCleared());
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
