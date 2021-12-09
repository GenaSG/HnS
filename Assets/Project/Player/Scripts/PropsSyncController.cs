using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSyncController : MonoBehaviour
{
    [SerializeField]
    private PropsSync propsSync;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) propsSync.SelectCurrentProp();
    }
}
