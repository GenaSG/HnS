using System;
using UnityEngine;

public class EventComponent : MonoBehaviour
{
    public event Action OnEvent = delegate { };
    public void Raise() => OnEvent();
}
