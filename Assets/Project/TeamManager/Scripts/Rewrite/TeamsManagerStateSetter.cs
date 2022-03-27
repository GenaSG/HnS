using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsManagerStateSetter : MonoBehaviour
{
    [SerializeField]
    TeamsManager manager;
    [SerializeField]
    TeamsManagerState state;
    // Start is called before the first frame update
    void Start()
    {
        manager.SetState(state);
    }

}
