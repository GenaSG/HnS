using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelector : MonoBehaviour
{
    [SerializeField]
    private DataList<GameObject> players;
    [SerializeField]
    private Team team;
    private void OnEnable()
    {
        if (!(players != null && players.Count != 0)) return;
        players[Random.Range(0, players.Count - 1)].GetComponent<CharacterTeam>().SetTeam(team);
    }
}
