using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private CharacterController character;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(character && target)
        {
            character.enabled = false;
            target.localPosition = position;
            character.enabled = true;
        }
        else if (!character && target)
        {
            target.localPosition = position;
        }
    }

}
