using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterMotor : MonoBehaviour
{
    private CharacterController character;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float jumpSpeed = 10f;
    [SerializeField]
    private float movementAcceleration = 10f;
    private Vector3 velocity;
    private Vector3 desiredDir;

    // Start is called before the first frame update
    void Awake()
    {
        character = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (character.isGrounded)
        {
            Grounded();
        }
        else
        {
            Airbourne();
        }
    }

    private void Airbourne()
    {
        velocity += Physics.gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
        velocity = character.velocity;
    }

    private void Grounded()
    {
        float checkDistance = character.height / 2 + character.skinWidth * 2;
        Vector3 startPosition = character.transform.position;
        Vector3 endPosition = startPosition + Vector3.down * checkDistance;
        RaycastHit hit;
        bool foundGround = Physics.Linecast(startPosition, endPosition, out hit, groundLayers);
        Vector3 normal = foundGround ? hit.normal : Vector3.up;
        Vector3 inputsDir = GetMoveDir();
        Vector3 dir = transform.forward * inputsDir.z + transform.right * inputsDir.x;
        dir *= moveSpeed;
        desiredDir = Vector3.MoveTowards(desiredDir, dir, movementAcceleration * Time.deltaTime);

        dir = Vector3.ProjectOnPlane(desiredDir, normal);
        if (GetJump())
        {
            dir += Vector3.up * jumpSpeed;
        }
        dir += Vector3.down;
        character.Move(dir * Time.deltaTime);
        velocity = character.velocity;
    }

    private bool GetJump()
    {
        return Input.GetButtonDown("Jump");
    }

    private Vector3 GetMoveDir()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
