using System;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Builders.SimpleCity;
using UnityEngine;


[RequireComponent(typeof(SimpleCityDungeonConfig)),
RequireComponent(typeof(Dungeon))]
public class MapController : DungeonEventListener
{
    public static event Action OnAnyMapGenerated = delegate { };
    public event Action OnMapGenerated = delegate { };
    [SerializeField]
    private DataList<GameObject> Props;
    [SerializeField]
    private DataList<GameObject> MapObjects;
    
    [SerializeField]
    private string PropsTags = "Props";
    
    private PrefabEventListener prefabEventListener;


    private Dungeon dungeon;

    public class PrefabEventListener : DungeonItemSpawnListener
    {
        public DataList<GameObject> Props;
        public DataList<GameObject> MapObjects;
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

    #endregion

    #region Cleanup
    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        Props.Clear();
        MapObjects.Clear();
    }
    #endregion


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
        OnAnyMapGenerated();
        OnMapGenerated();
    }

    public override void OnDungeonDestroyed(Dungeon dungeon)
    {
        base.OnDungeonDestroyed(dungeon);
        Props.Clear();
        MapObjects.Clear();
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
