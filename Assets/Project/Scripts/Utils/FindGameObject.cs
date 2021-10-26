using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FindGameObject
{
    internal enum SearchType { ByName, ByTag, ByHierarchyPath};
    [SerializeField]
    SearchType searchType;
    [SerializeField]
    string gameObjectName = "";
    [SerializeField]
    string gameObjectTag = "";
    [SerializeField]
    bool cacheResults;
    [SerializeField]
    GameObject found;
    public GameObject Object
    {
        get
        {
            if (cacheResults && found != null) return found;
            if (searchType == SearchType.ByName)
            {
                var g = GameObject.Find(gameObjectName);
                if (cacheResults) found = g;
                return g;
            }
            else if (searchType == SearchType.ByTag)
            {
                var g = GameObject.FindGameObjectWithTag(gameObjectTag);
                if (cacheResults) found = g;
                return g;
            }
            return null;
        }
    }
}
