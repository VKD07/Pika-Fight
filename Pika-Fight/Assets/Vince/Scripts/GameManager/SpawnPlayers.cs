using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] Transform[] playerSpawners;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] PlayerDataDictionary playerDataDictionary;
    [SerializeField] int numberOfPlayers;

    private void Awake()
    {
        //putting all the values in the dictionary
        playerDataDictionary.InitDictionary();
    }

    void Start()
    {
        GetNumberOfPlayers();
        SpawnCharacters();
    }

    private void GetNumberOfPlayers()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null)
            {
                numberOfPlayers++;
            }
        }
    }

    void SpawnCharacters()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            int randomPos = Random.Range(0, playerSpawners.Length);

            GameObject player = Instantiate(playerPrefab, playerSpawners[randomPos].position, Quaternion.identity);

            player.transform.Find(playerJoinedData.GetPlayersJoined[i].CharacterName + "_Head").gameObject.SetActive(true);
            player.transform.Find(playerJoinedData.GetPlayersJoined[i].CharacterName + "_Body").gameObject.SetActive(true);

            player.GetComponent<PlayerConfigBridge>().SetPlayerConfig = playerJoinedData.GetPlayersJoined[i];
            player.GetComponent<Rigidbody>().isKinematic = false;

            //enabling all scripts
            player.GetComponent<PlayerStatus>().EnableScripts(true);
            player.GetComponent<DodgeBall>().enabled = false;
            //player.GetComponent<Animator>().SetBool("DodgeBall", true);
            SetPlayerControlsToPlayerScripts(player,playerJoinedData.GetPlayersJoined[i].Player_Controls);
            SetPlayerVelocityVariable(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName].playerVeloctiy);
            SetPlayerMovementSpeed(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName].movementSpeed);
            SetPlayerAnimationData(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName].playerAnimData);
            SetPlayerHealth(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName].playerHealth);
        }
    }

    public void SetPlayerControlsToPlayerScripts(GameObject player, PlayerControls playerControls)
    {
        player.GetComponent<PlayerMovement>().SetPlayerControls = playerControls;
        player.GetComponent<DodgeBall>().SetPlayerControls = playerControls;
        player.GetComponent<PlayerAnimation>().SetPlayerControls = playerControls;
        player.GetComponent<Dash>().SetPlayerControls = playerControls;
        player.GetComponent<MeleeFight>().SetPlayerControls = playerControls;
    }

    void SetPlayerVelocityVariable(GameObject player, FloatReference playerVelocityVar)
    {
        player.GetComponent<PlayerMovement>().PlayerVelocity = playerVelocityVar;
        player.GetComponent<DodgeBall>().PlayerVelocity = playerVelocityVar;
        player.GetComponent<PlayerAnimation>().PlayerVelocity = playerVelocityVar;
    }

    void SetPlayerMovementSpeed(GameObject player, FloatReference playerMovementSpeed)
    {
        player.GetComponent<PlayerMovement>().PlayerMovementSpeed = playerMovementSpeed;
        player.GetComponent<DodgeBall>().PlayerMovementSpeed = playerMovementSpeed;
    }

    void SetPlayerHealth(GameObject player, FloatReference playerHealth)
    {
        player.GetComponent<HealthBar>().PlayerHealth = playerHealth;
        player.GetComponent<ReceiveDamage>().PlayerHealth = playerHealth;
        player.GetComponent<PlayerStatus>().PlayerHealth = playerHealth;
        player.GetComponent<HealthBar>().healthValue = 100f;
    }

    void SetPlayerAnimationData(GameObject player, PlayerAnimationData playerAnimData)
    {
        player.GetComponent<DodgeBall>().PlayerAnimData = playerAnimData;
        player.GetComponent<PlayerAnimation>().PlayerAnimData = playerAnimData;
    }


}
