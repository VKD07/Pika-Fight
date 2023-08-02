using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheChicken : MonoBehaviour
{
    [SerializeField] GameObject[] playerInScene;
    [SerializeField] List<GameObject> normalPlayers;
    [SerializeField] float timeToSelectADifferentPlayer = 10f;
    [SerializeField] int randomPlayer;
    [SerializeField] GameObject currentChicken;
    float currentTime;
    bool startChoosingOthers;

    void Start()
    {
        FindPlayersInSceneAndAddItTotheList();
        StartCoroutine(TransformPlayerIntoChicken(2));
    }

    void Update()
    {
        TimerToSwitchPlayer();
        DisablePlayersRecevingDamage();
    }

    IEnumerator TransformPlayerIntoChicken(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (playerInScene != null)
        {
            RandomizePlayer();
            normalPlayers[randomPlayer].GetComponentInChildren<ChickenMode>().enabled = true;
            currentChicken = normalPlayers[randomPlayer];
            StartCoroutine(RemoveCurrentChickenFromThelist());
            startChoosingOthers = true;
        }
    }

    private void TimerToSwitchPlayer()
    {
        if (normalPlayers.Count == 0)
        {
            AddPlayersInTheList();
        }
        if (currentTime < timeToSelectADifferentPlayer && startChoosingOthers)
        {
            currentTime += Time.deltaTime;
        }
        else if (currentTime >= timeToSelectADifferentPlayer && startChoosingOthers)
        {
            currentTime = 0;
            TransformPlayerToNormal();
            StartCoroutine(TransformPlayerIntoChicken(0.5f));
        }
    }

    void TransformPlayerToNormal()
    {
        currentChicken.GetComponentInChildren<ChickenMode>().enabled = false;
    }

    IEnumerator RemoveCurrentChickenFromThelist()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentChicken != null)
        {
            for (int i = 0; i < normalPlayers.Count; i++)
            {
                if (currentChicken == normalPlayers[i])
                {
                    normalPlayers.Remove(normalPlayers[i]);
                }
            }
        }
    }

    void RandomizePlayer()
    {
        randomPlayer = UnityEngine.Random.Range(0, normalPlayers.Count);
    }

    private void FindPlayersInSceneAndAddItTotheList()
    {
        playerInScene = GameObject.FindGameObjectsWithTag("Player");
        AddPlayersInTheList();
    }

    void AddPlayersInTheList()
    {
        for (int i = 0; i < playerInScene.Length; i++)
        {
            normalPlayers.Add(playerInScene[i]);
        }
    }

    void DisablePlayersRecevingDamage()
    {
        for (int i = 0; i < playerInScene.Length; i++)
        {
            playerInScene[i].GetComponent<ReceiveDamage>().enabled = false;
        }
    }
}
