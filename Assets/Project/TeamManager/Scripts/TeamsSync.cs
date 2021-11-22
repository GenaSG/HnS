using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class TeamsSync : NetworkBehaviour
{
    [SerializeField]
    private DataList<uint> playerIDs;
    [SerializeField]
    private DataList<uint> hiderIDs;
    [SerializeField]
    private DataList<uint> seekerIDs;
    //[SerializeField]
    //private DataList<GameObject> players;
    //[SerializeField]
    //private DataList<GameObject> hiders;
    //[SerializeField]
    //private DataList<GameObject> seekers;

    //private SyncList<GameObject> syncedPlayers = new SyncList<GameObject>();
    //private SyncList<GameObject> syncedHiders = new SyncList<GameObject>();
    //private SyncList<GameObject> syncedSeekers = new SyncList<GameObject>();

    private SyncList<uint> syncedPlayerIDs = new SyncList<uint>();
    private SyncList<uint> syncedHiderIDs = new SyncList<uint>();
    private SyncList<uint> syncedSeekerIDs = new SyncList<uint>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        //players.Init((IList<GameObject>)syncedPlayers);
        //hiders.Init((IList<GameObject>)syncedHiders);
        //seekers.Init((IList<GameObject>)syncedSeekers);
        playerIDs.Init((IList<uint>)syncedPlayerIDs);
        hiderIDs.Init((IList<uint>)syncedHiderIDs);
        seekerIDs.Init((IList<uint>)syncedSeekerIDs);
    }

    private void OnDisable()
    {
        
    }
}
