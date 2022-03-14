using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class MapInventory : MonoBehaviour
{
    private readonly static HashSet<GameObject> props;
    private readonly static HashSet<GameObject> mapObjects;

    public HashSet<GameObject> Props => props;
    public HashSet<GameObject> MapObjects => mapObjects;

    static MapInventory()
    {
        EventBus<OnMapGenerated>.Subscribe(MapGenerated);
        EventBus<OnMapCleared>.Subscribe(MapCleared);
        props = new HashSet<GameObject>();
        mapObjects = new HashSet<GameObject>();
    }

    private static void MapGenerated(object caller, OnMapGenerated map)
    {
        foreach(GameObject go in map.props)
        {
            if (!props.Contains(go)) props.Add(go);
        }
        foreach (GameObject go in map.levelGeometry)
        {
            if (!mapObjects.Contains(go)) mapObjects.Add(go);
        }
    }

    private static void MapCleared(object caller, OnMapCleared empty)
    {
        props.Clear();
        mapObjects.Clear();
    }

}
