using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectStateSync : MonoBehaviour
{
    private void OnTransformChildrenChanged()
    {
        Debug.Log("Changed");
    }
}
