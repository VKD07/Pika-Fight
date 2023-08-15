using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] PlayerDataDictionary playerDataDictionary;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float timeToSpawn = 2f;
    //[SerializeField] int numberOfPlayers;
    [SerializeField] List<Vector3> playerSpawners;
    [SerializeField] UnityEvent OnSpawn;
    float numberOfPlayers;
    private void Awake()
    {
        //putting all the values in the dictionary
        playerDataDictionary.InitDictionary();

    }

    void Start()
    {
        numberOfPlayers = playerJoinedData.GetNumberOfPlayersJoined();
        //GetNumberOfPlayers();
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
            GameObject player = Instantiate(playerPrefab, playerSpawners[i], Quaternion.identity);

            //set game object name
            player.name = $"{playerJoinedData.GetPlayersJoined[i].name}_{playerJoinedData.GetPlayersJoined[i].CharacterName}";
            //enable specific character 3D model
            EnableCharacterModel(player, playerJoinedData.GetPlayersJoined[i].CharacterName.ToString());
            //setting player UI identifier color
            player.GetComponent<PlayerIdentifierUI>().PlayerColor = playerJoinedData.GetPlayersJoined[i].PlayerColor;
            
            player.GetComponent<PlayerConfigBridge>().PlayerConfig = playerJoinedData.GetPlayersJoined[i];
            player.GetComponent<Rigidbody>().isKinematic = false;
            OnSpawn.Invoke();

            //enabling all scripts
            player.GetComponent<PlayerStatus>().EnableScripts(true);
            ////Setting player data from player data dictionary
            SetPlayerControlsToPlayerScripts(player, playerJoinedData.GetPlayersJoined[i].Player_Controls);
            SetPlayerVelocityVariable(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName.ToString()].playerVeloctiy);
            SetPlayerMovementSpeed(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName.ToString()].movementSpeed);
            SetPlayerAnimationData(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName.ToString()].playerAnimData);
            SetPlayerHealth(player, playerDataDictionary.myDict[playerJoinedData.GetPlayersJoined[i].CharacterName.ToString()].playerHealth);
        }
    }

    void EnableCharacterModel(GameObject player, string characterName)
    {
        player.transform.Find($"{characterName}_Head").gameObject.SetActive(true);
        player.transform.Find($"{characterName}_Body").gameObject.SetActive(true);
    }


    void SetPlayerControlsToPlayerScripts(GameObject player, PlayerControls playerControls)
    {
        player.GetComponent<PlayerMovement>().SetPlayerControls = playerControls;
        player.GetComponentInChildren<DodgeBall>().SetPlayerControls = playerControls;
        player.GetComponent<PlayerAnimation>().SetPlayerControls = playerControls;
        player.GetComponent<Dash>().SetPlayerControls = playerControls;
        player.GetComponentInChildren<MeleeFight>().SetPlayerControls = playerControls;
    }

    void SetPlayerVelocityVariable(GameObject player, FloatReference playerVelocityVar)
    {
        player.GetComponent<PlayerMovement>().PlayerVelocity = playerVelocityVar;
        player.GetComponentInChildren<DodgeBall>().PlayerVelocity = playerVelocityVar;
        player.GetComponent<PlayerAnimation>().PlayerVelocity = playerVelocityVar;
        player.GetComponentInChildren<PickUpChicken>().PlayerVelocity = playerVelocityVar;
        player.GetComponentInChildren<ChickenMode>().PlayerVelocity = playerVelocityVar;
    }

    void SetPlayerMovementSpeed(GameObject player, FloatReference playerMovementSpeed)
    {
        player.GetComponent<PlayerMovement>().PlayerMovementSpeed = playerMovementSpeed;
        player.GetComponentInChildren<DodgeBall>().PlayerMovementSpeed = playerMovementSpeed;
        player.GetComponentInChildren<ChickenMode>().PlayerMovementSpeed = playerMovementSpeed;
    }

    void SetPlayerHealth(GameObject player, FloatReference playerHealth)
    {
        player.GetComponent<HealthBar>().PlayerHealth = playerHealth;
        player.GetComponent<ReceiveDamage>().PlayerHealth = playerHealth;
        player.GetComponent<PlayerStatus>().PlayerHealth = playerHealth;
        player.GetComponentInChildren<PickUpChicken>().PlayerHealth = playerHealth;
        player.GetComponent<Stun>().PlayerHealth = playerHealth;    
        player.GetComponent<HealthBar>().healthValue = 100f;
    }

    void SetPlayerAnimationData(GameObject player, PlayerAnimationData playerAnimData)
    {
        player.GetComponentInChildren<DodgeBall>().PlayerAnimData = playerAnimData;
        player.GetComponent<PlayerAnimation>().PlayerAnimData = playerAnimData;
        player.GetComponent<Stun>().PlayerAnimationData = playerAnimData;
    }

    public void SpawnAPlayer()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playersFound in players)
        {
            if (playersFound.GetComponent<PlayerConfigBridge>().PlayerConfig.PlayerIsDead)
            {
                playersFound.GetComponent<HealthBar>().healthValue = 100f;
                playersFound.GetComponent<PlayerStatus>().ReEnableScriptsAfterRespawning();
                playersFound.GetComponent<Rigidbody>().isKinematic = false;
                ResetPlayerData(playersFound.GetComponent<PlayerConfigBridge>().PlayerConfig);
                SpawnPlayerRandomly(playersFound);
            }
        }
    }
    
    void ResetPlayerData(PlayerConfig playerConfig)
    {
        playerConfig.GemScore = 0;
        playerConfig.PlayerIsDead = false;
    }

    void SpawnPlayerRandomly(GameObject player)
    {
        int randomPos = Random.Range(0, playerSpawners.Count);
        player.transform.position = playerSpawners[randomPos];
    }
    public void MoveSpawnPoint(int i, Vector3 pos)
    {
        playerSpawners[i] = pos;
    }

    public Transform SpawnerParent { get => SpawnerParent; }
    public List<Vector3> SpawnPoints { get => playerSpawners;}
}
