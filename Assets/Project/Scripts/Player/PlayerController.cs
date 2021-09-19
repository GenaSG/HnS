using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(SimpleCharacterMotor)), RequireComponent(typeof(SimpleMouseLook)), RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private Camera camera;
    private SimpleCharacterMotor motor;
    private SimpleMouseLook mouseLook;
    private CharacterController character;


    // Start is called before the first frame update
    void Awake()
    {
        motor = GetComponent<SimpleCharacterMotor>();
        mouseLook = GetComponent<SimpleMouseLook>();
        character = GetComponent<CharacterController>();
        motor.enabled = false;
        mouseLook.enabled = false;
        camera.enabled = false;
        character.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        motor.enabled = true;
        mouseLook.enabled = true;
        camera.enabled = true;
        character.enabled = true;
    }


}
