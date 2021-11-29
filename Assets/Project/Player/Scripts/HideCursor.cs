using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
    [SerializeField]
    private CursorLockMode cursorLockMode;
    [SerializeField]
    private bool hideCursor;
    private bool originalHideCursor;
    private CursorLockMode original;
    private void OnEnable()
    {
        original = Cursor.lockState;
        originalHideCursor = Cursor.visible;
        Cursor.lockState = cursorLockMode;
        Cursor.visible = hideCursor;
    }

    private void OnDisable()
    {
        Cursor.lockState = original;
        Cursor.visible = originalHideCursor;
    }
}
