using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MultiplayerPlayerController : NetworkBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private AudioListener listener;
    [SerializeField]
    private SimpleCharacterController motor;
    [SerializeField]
    private CharacterController character;
    [SerializeField]
    private MultiplayerPlayerControllerState propStrategy;


    // Start is called before the first frame update
    void Awake()
    {
        motor.enabled = false;
        camera.enabled = false;
        listener.enabled = false;
        character.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        motor.enabled = true;
        camera.enabled = true;
        listener.enabled = true;
        character.enabled = true;
    }

    private void Update()
    {
        propStrategy.StrategyUpdate();
    }

}
