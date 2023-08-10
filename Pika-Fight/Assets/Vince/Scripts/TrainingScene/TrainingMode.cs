using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMode : MonoBehaviour
{
    [SerializeField] GameObject[] players; 
    float numberOfPlayersJoined;
    void Start()
    {
        StartCoroutine(EnableDodgeBall());
    }

    void Update()
    {
        DisablePlayersReceivingDamage();
    }

    private void DisablePlayersReceivingDamage()
    {
        if (players != null)
        {
            foreach (var player in players)
            {
                player.GetComponent<ReceiveDamage>().enabled = false;
            }
        }
    }

    IEnumerator EnableDodgeBall()
    {
        yield return new WaitForSeconds(2f);
        FindPlayers();
        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponentInChildren<DodgeBall>().enabled = true;
            }
        }
    }
    void FindPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
