using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownActivator : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    string characterName;
    GameObject characterHead;
    public int highestScorePlayerIndex;
    public int highestScore = 1;
    bool isTie = false;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        HideAllCrowns();
        StartCoroutine(PlaceCrownToHighestScorePlayer());
    }

    void HideAllCrowns()
    {
        foreach (var player in players)
        {
            player.GetComponentInChildren<ActivateCrown>().ActivateTheCrown(false);
        }
    }

    IEnumerator PlaceCrownToHighestScorePlayer()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerConfigBridge>().PlayerConfig.PlayerScore > highestScore)
            {
                highestScore = players[i].GetComponent<PlayerConfigBridge>().PlayerConfig.PlayerScore;
                highestScorePlayerIndex = i;
                isTie = false;
            }
            else if (players[i].GetComponent<PlayerConfigBridge>().PlayerConfig.PlayerScore == highestScore)
            {
                isTie = true;
            }
        }
        players[highestScorePlayerIndex].GetComponentInChildren<ActivateCrown>().ActivateTheCrown(!isTie);
    }
}
