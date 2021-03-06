using UnityEngine;
using Mirror;

public class NetworkedMapGeneration : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnSeedUpdated))]
    private uint seed;
    
    [SerializeField]
    private MapController mapController;


    public override void OnStartServer()
    {
        base.OnStartServer();
        seed = (uint)Random.Range(0, 2147483640);
        mapController.BuildMap(seed);
        
    }

    [ClientCallback]
    void OnSeedUpdated(uint lastSeed, uint currentSeed)
    {
        mapController.BuildMap(currentSeed);
    }

}
