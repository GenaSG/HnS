using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NewPlayerPresentationInfo
{
    public GameObject presentationObject;
    public GameObject cameraTarget;
}

public class OnNewPlayerPresentationCreatedChannel : GenericChannel<OnNewPlayerPresentationCreatedChannel, NewPlayerPresentationInfo>
{

}
