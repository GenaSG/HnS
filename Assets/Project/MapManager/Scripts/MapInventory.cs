using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class MapInventory : MonoBehaviour
{
    private static GameObject[] props;
    private static GameObject[] levelGeometry;
    private readonly static Dictionary<GameObject, uint> propsInventory;
    private readonly static Dictionary<GameObject, uint> levelGeometryInventory;


    static MapInventory()
    {
        EventBus<OnMapGenerated>.Subscribe(MapGenerated);
        EventBus<OnMapCleared>.Subscribe(MapCleared);
        propsInventory = new Dictionary<GameObject, uint>();
        levelGeometryInventory = new Dictionary<GameObject, uint>();
    }

    private static void MapGenerated(object caller, OnMapGenerated map, object target)
    {
        props = map.props;
        levelGeometry = map.levelGeometry;
        for(uint i = 0; i < props.Length; i++)
        {
            if (propsInventory.ContainsKey(props[i])) continue;
            propsInventory.Add(props[i], i);
        }
        for (uint i = 0; i < levelGeometry.Length; i++)
        {
            if (levelGeometryInventory.ContainsKey(levelGeometry[i])) continue;
            levelGeometryInventory.Add(levelGeometry[i], i);
        }
    }

    private static void MapCleared(object caller, OnMapCleared empty, object target)
    {
        propsInventory.Clear();
        levelGeometryInventory.Clear();
        props = new GameObject[0];
        levelGeometry = new GameObject[0];
    }


    public bool LevelGeometryContainsIndex(uint index)
    {
        return index < levelGeometry.Length;
    }

    public GameObject GetLevelGeometryForIndex(uint index)
    {
        return levelGeometry[index];
    }

    public bool LevelGeometryContains(GameObject geometry) => levelGeometryInventory.ContainsKey(geometry);

    public uint GetIndexForLevelGeometry(GameObject geometry)
    {
        return levelGeometryInventory[geometry];
    }

    public bool PropsContainsIndex(uint index)
    {
        return index < props.Length;
    }

    public GameObject GetPropForIndex(uint index)
    {
        return props[index];
    }

    public bool PropsContains(GameObject prop) => propsInventory.ContainsKey(prop);

    public uint GetIndexForProp(GameObject prop)
    {
        return propsInventory[prop];
    }


}
