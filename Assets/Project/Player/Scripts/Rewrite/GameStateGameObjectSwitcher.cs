using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;


public class GameStateGameObjectSwitcher : MonoBehaviour
{
    [System.Serializable]
    internal class KeyValue<TKey,TValue>
    {
        public TKey key;
        public TValue value;
    }
    [SerializeField]
    private List<KeyValue<Object, GameObject>> stateToGameObject = new List<KeyValue<Object, GameObject>>();
    
    private Dictionary<Object, GameObject> inventory = new Dictionary<Object, GameObject>();

    private void OnEnable()
    {
        inventory.Clear();
        foreach (KeyValue<Object, GameObject> kv in stateToGameObject)
        {
            inventory.Add(kv.key, kv.value);
        }
        EventBus<OnGameStateChanged>.Subscribe(GameStateChanged);
    }

    private void OnDisable()
    {
        EventBus<OnGameStateChanged>.UnSubscribe(GameStateChanged);
    }

    void GameStateChanged(object caller, OnGameStateChanged stateChanged,object target)
    {
        if (!inventory.ContainsKey(stateChanged.newState)) return;
        foreach (KeyValue<Object, GameObject> kv in stateToGameObject)
        {
            kv.value.SetActive(false);
        }
        inventory[stateChanged.newState].SetActive(true);
    }

}
