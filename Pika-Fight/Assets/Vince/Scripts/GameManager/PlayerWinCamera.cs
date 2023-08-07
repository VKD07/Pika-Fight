using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCamera : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject winBanner;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void EnableWinCamera()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerConfigBridge>().PlayerConfig.Winner)
            {
                players[i].transform.Find("WinCamera").gameObject.SetActive(true);
                players[i].GetComponent<Animator>().SetTrigger("ModeWinner");
                players[i].GetComponent<PlayerMovement>().enabled = false;
                winBanner.SetActive(true);
                break;
            }
        }
    }
}
