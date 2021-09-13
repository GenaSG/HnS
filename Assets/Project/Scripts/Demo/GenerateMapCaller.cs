using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GenerateMapWithSeedChannel))]
public class GenerateMapCaller : MonoBehaviour
{
    [SerializeField]
    private KeyCode regenerateButton;
    private GenerateMapWithSeedChannel channel;

    private void Awake()
    {
        channel = GetComponent<GenerateMapWithSeedChannel>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(regenerateButton)) channel.Invoke((object)this, (uint)Random.Range(0, 99999999));
    }
}
