using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject[] playersInScene;
    [SerializeField] List<GameObject> playersAlive;
    [SerializeField] string positionName;
    [SerializeField] float bombDuration = 20f;
    [SerializeField] GameObject explosionVfx;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] FloatReference numOfPlayersDead;
    [SerializeField] float numOfPlayersJoined;
    [SerializeField] int randomPlayer;
    [SerializeField] GameObject playerCarrier;
    bool transfered;
    float currentTime;

    private void Start()
    {
        playersInScene = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < playersInScene.Length; i++)
        {
            playersAlive.Add(playersInScene[i]);
        }

        numOfPlayersJoined = playerJoinedData.GetNumberOfPlayersJoined();
        StartCoroutine(RandomizePlayerCarrier());
        DisablePlayersRecevingDamage();
    }
    // Update is called once per frame
    void Update()
    {
        SetPosToPlayer();
        StartBombCountDown();
    }

    void SetPosToPlayer()
    {
        if (playersInScene.Length > 0 && playerCarrier != null && playerCarrier.GetComponent<HealthBar>().healthValue > 0)
        {
            transform.position = playerCarrier.transform.Find("root/pelvis/spine_01/" + positionName).position;
        }
    }

    void StartBombCountDown()
    {
        if (playerCarrier != null)
        {
            if (currentTime < bombDuration)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                ExplosionVfx();
                playerCarrier.GetComponent<HealthBar>().healthValue = 0f;
                playerCarrier.GetComponent<PlayerConfigBridge>().PlayerConfig.PlayerIsDead = true;
                currentTime = 0;
                RemovePlayerCarrierFromAliveList();
                StartCoroutine(CheckIfTheresMoreThanOnePlayersAlive());
            }
        }
    }

    IEnumerator CheckIfTheresMoreThanOnePlayersAlive()
    {
        yield return new WaitForSeconds(0);
        if (numOfPlayersDead.Value >= numOfPlayersJoined - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //randomize again if theres remaining players left
            StartCoroutine(RandomizePlayerCarrier());
        }
    }

    void RemovePlayerCarrierFromAliveList()
    {
        for (int i = 0; i < playersAlive.Count; i++)
        {
            if (playersAlive[i] == playerCarrier)
            {
                playersAlive.Remove(playersAlive[i]);
            }
        }
    }

    IEnumerator RandomizePlayerCarrier()
    {
        yield return new WaitForSeconds(0);
        randomPlayer = Random.Range(0, playersAlive.Count);
        playerCarrier = playersAlive[randomPlayer];
    }

    void DisablePlayersRecevingDamage()
    {
        for (int i = 0; i < playersInScene.Length; i++)
        {
            playersInScene[i].GetComponent<ReceiveDamage>().enabled = false;
        }
    }

    void ExplosionVfx()
    {
        GameObject explosion = Instantiate(explosionVfx, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCarrier = other.gameObject;
        }
    }
}
