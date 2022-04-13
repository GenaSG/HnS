using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSelectionInputs : MonoBehaviour
{
    [SerializeField]
    private PlayerPropSelector selector;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) selector.SelectCurrentProp();
    }
}
