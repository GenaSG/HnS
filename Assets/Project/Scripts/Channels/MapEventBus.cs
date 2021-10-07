using UnityEngine;

public class MapEventBus : MonoBehaviour
{
    [System.Serializable]
    public class OnGenerateMapWithSeed : Channel<OnGenerateMapWithSeed, uint> { }
    public readonly OnGenerateMapWithSeed onGenerateMapWithSeed = new OnGenerateMapWithSeed();
    [System.Serializable]
    public class OnMapGenerated : Channel<OnMapGenerated> { }
    public readonly OnMapGenerated onMapGenerated = new OnMapGenerated();

}
