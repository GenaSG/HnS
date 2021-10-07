using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(MapEventBus))]
public class NetworkedMapGeneration : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnSeedUpdated))]
    private uint seed;
    private MapEventBus mapEventBus;

    // Start is called before the first frame update
    void Awake()
    {
        mapEventBus = GetComponent<MapEventBus>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        seed = (uint)Random.Range(0, 2147483640);
        mapEventBus.onGenerateMapWithSeed.Invoke((object)this, seed);
    }

    [ClientCallback]
    void OnSeedUpdated(uint lastSeed, uint currentSeed)
    {
        mapEventBus.onGenerateMapWithSeed.Invoke((object)this, currentSeed);
    }

}
