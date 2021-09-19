using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MultiplayerPlayerController : NetworkBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private SimpleCharacterController motor;
    [SerializeField]
    private CharacterController character;


    // Start is called before the first frame update
    void Awake()
    {
        motor.enabled = false;
        camera.enabled = false;
        character.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        motor.enabled = true;
        camera.enabled = true;
        character.enabled = true;
    }


}
