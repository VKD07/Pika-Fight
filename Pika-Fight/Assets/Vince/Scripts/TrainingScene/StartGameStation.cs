using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameStation : MonoBehaviour
{
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] Slider startGameSlider;
    [SerializeField] TextMeshProUGUI numofPlayersTxt;
    [SerializeField] float timeToStart = 5f;
    [SerializeField] UnityEvent OnStartGame;
    float currentTime;
    float numOfPlayersJoined;
    int numOfPlayersInTheArea;
    private void Start()
    {
        numOfPlayersJoined = playerJoinedData.GetNumberOfPlayersJoined();
        numofPlayersTxt.SetText($"{numOfPlayersInTheArea}/{numOfPlayersJoined}");
        startGameSlider.maxValue = timeToStart;
    }

    private void Update()
    {
        Timer();
        startGameSlider.value = currentTime;
        numofPlayersTxt.SetText($"{numOfPlayersInTheArea}/{numOfPlayersJoined}");
    }

    void Timer()
    {
        if(currentTime < timeToStart && numOfPlayersInTheArea >= numOfPlayersJoined)
        {
            currentTime += Time.deltaTime;
        }
        else if(numOfPlayersInTheArea < numOfPlayersJoined)
        {
            currentTime = 0;
        }
        else if(currentTime >= timeToStart && numOfPlayersInTheArea >= numOfPlayersJoined)
        {
            OnStartGame.Invoke();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            numOfPlayersInTheArea += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            numOfPlayersInTheArea -= 1;
        }
    }
}
