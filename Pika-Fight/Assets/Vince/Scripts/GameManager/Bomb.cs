using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject []playersInScene;
    [SerializeField] string positionName;
    [SerializeField] float bombDuration = 20f;
    int randomPlayer;
    GameObject playerCarrier;
    bool transfered;
    private void Start()
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
        if(playersInScene.Length > 0)
        {
            transform.position = playerCarrier.transform.Find("root/pelvis/spine_01/"+positionName).position;
        }
    }

    void StartBombCountDown()
    {

    }

    void Randomize()
    {
        randomPlayer = Random.Range(0, playersInScene.Length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerCarrier = other.gameObject;
        }
    }
}
