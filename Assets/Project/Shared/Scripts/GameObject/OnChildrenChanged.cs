using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnChildrenChanged : MonoBehaviour
{
    public event Action OnChildrenChange = delegate {};
    private void OnTransformChildrenChanged()
    {
        OnChildrenChange();
    }
}
