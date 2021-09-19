using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform root;
    [SerializeField]
    private float sensitivity = 3f;

    private float deltaX;
    private float deltaY;

    private float startX;
    private float startY;

    private void Start()
    {
        startX = head.localEulerAngles.x;
        startY = root.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        deltaX -= Input.GetAxis("Mouse Y") * sensitivity;
        deltaY += Input.GetAxis("Mouse X") * sensitivity;

        float x = Mathf.Clamp(startX + deltaX, -90f, 90f);
        head.localEulerAngles = new Vector3(x, head.localEulerAngles.y, head.localEulerAngles.z);
        
        root.localEulerAngles = new Vector3(root.localEulerAngles.z, startY + deltaY, root.localEulerAngles.z);
    }
}
