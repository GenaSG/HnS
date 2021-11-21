using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class TeamsSync : NetworkBehaviour
{
    [SerializeField]
    private DataList<GameObject> players;
    [SerializeField]
    private DataList<GameObject> hiders;
    [SerializeField]
    private DataList<GameObject> seekers;

    private SyncList<GameObject> syncedPlayers = new SyncList<GameObject>();
    private SyncList<GameObject> syncedHiders = new SyncList<GameObject>();
    private SyncList<GameObject> syncedSeekers = new SyncList<GameObject>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        players.Init((IList<GameObject>)syncedPlayers);
        hiders.Init((IList<GameObject>)syncedHiders);
        seekers.Init((IList<GameObject>)syncedSeekers);
        
    }

    private void OnDisable()
    {
        
    }
}
