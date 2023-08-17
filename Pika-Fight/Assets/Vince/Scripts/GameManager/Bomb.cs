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
    [SerializeField] UnityEvent OnExplosion;
    [SerializeField] UnityEvent OnChangeCarrier;
    [SerializeField] float bombStartDelay = 5f;
    UIIndicator uIIndicator;
    bool transfered;
    float currentTime;

    private void Start()
    {
        uIIndicator = GetComponent<UIIndicator>();
        playersInScene = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playersInScene.Length; i++)
        {
            playersAlive.Add(playersInScene[i]);
        }

        numOfPlayersJoined = playerJoinedData.GetNumberOfPlayersJoined();
        StartCoroutine(RandomizePlayerCarrier(bombStartDelay));
    }
    // Update is called once per frame
    void Update()
    {
        SetPosToPlayer();
        StartBombCountDown();
        DisablePlayersRecevingDamage();
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
            uIIndicator.SetActiveUIIndicator(false);
            Destroy(gameObject);
        }
        else
        {
            //randomize again if theres remaining players left
            StartCoroutine(RandomizePlayerCarrier(0));
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

    IEnumerator RandomizePlayerCarrier(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        randomPlayer = Random.Range(0, playersAlive.Count);
        playerCarrier = playersAlive[randomPlayer];
        EnableBombUiIndicator();
    }

    void DisablePlayersRecevingDamage()
    {
        for (int i = 0; i < playersInScene.Length; i++)
        {
            playersInScene[i].GetComponent<ReceiveDamage>().enabled = false;
        }
    }

    void EnableBombUiIndicator()
    {
        if(playerCarrier != null)
        {
            uIIndicator.UILocation = playerCarrier.transform.Find("PlayerUILocation");
            uIIndicator.TriggerAnimation();
        }
    }

    void ExplosionVfx()
    {
        OnExplosion.Invoke();
        GameObject explosion = Instantiate(explosionVfx, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<HealthBar>().healthValue > 0)
        {
            OnChangeCarrier.Invoke();
            playerCarrier = other.gameObject;
            EnableBombUiIndicator();
        }
    }

    public float CurrentTime => currentTime;
}
