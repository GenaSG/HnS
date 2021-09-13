using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(GenerateMapWithSeedChannel))]
public class NetworkedMapGeneration : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnSeedUpdated))]
    private uint seed;
    private GenerateMapWithSeedChannel channel;

    // Start is called before the first frame update
    void Awake()
    {
        channel = GetComponent<GenerateMapWithSeedChannel>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        seed = (uint)Random.Range(0, 2147483640);
        channel.Invoke((object)this, seed);
    }

    [ClientCallback]
    void OnSeedUpdated(uint lastSeed, uint currentSeed)
    {
        channel.Invoke((object)this, currentSeed);
    }

}
