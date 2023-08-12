using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWinCamera : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject winBanner;
    [SerializeField] UnityEvent OnWinnerFound;
    int index;
    void Start()
    {
    }

    private void Update()
    {
        FindPlayerWinner();
    }

    private void FindPlayerWinner()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerConfigBridge>().PlayerConfig.Winner)
            {
                index = i;
                break;
            }
        }
    }

    public void EnableWinCamera()
    {
        players[index].GetComponentInChildren<PlayerWinCameraEnabler>().EnableCamera();
        players[index].GetComponent<Animator>().SetTrigger("ModeWinner");
        players[index].GetComponent<PlayerMovement>().enabled = false;
        winBanner.SetActive(true);
        OnWinnerFound.Invoke();
    }
}
