using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject []playersInScene;
    [SerializeField] string positionName;
    [SerializeField] float bombDuration = 20f;
    [SerializeField] GameObject explosionVfx;
    float currentTime;
    [SerializeField]int randomPlayer;
    [SerializeField] GameObject playerCarrier;
    bool transfered;
    
    private void OnEnable()
    {
        playersInScene = GameObject.FindGameObjectsWithTag("Player");
        Randomize();
        playerCarrier = playersInScene[randomPlayer];
    }

    // Update is called once per frame
    void Update()
    {
        SetPosToPlayer();
        StartBombCountDown();
    }

    void SetPosToPlayer()
    {
        if(playersInScene.Length > 0 && playerCarrier != null && playerCarrier.GetComponent<HealthBar>().healthValue > 0)
        {
            transform.position = playerCarrier.transform.Find("root/pelvis/spine_01/"+positionName).position;
        }
    }

    void StartBombCountDown()
    {
        if(playerCarrier != null)
        {
            if(currentTime < bombDuration)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                ExplosionVfx();
                playerCarrier.GetComponent<ReceiveDamage>().GetDamage(100);
                playerCarrier.GetComponent<PlayerConfigBridge>().PlayerConfig.PlayerIsDead = true;
                Randomize();
                playerCarrier = playersInScene[randomPlayer];
                currentTime = 0;
            }
        }
    }

    void Randomize()
    {
        randomPlayer = Random.Range(0, playersInScene.Length);
    }

    void ExplosionVfx()
    {
        GameObject explosion = Instantiate(explosionVfx,transform.position, Quaternion.identity);
        Destroy(explosion,2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerCarrier = other.gameObject;
        }
    }
}
