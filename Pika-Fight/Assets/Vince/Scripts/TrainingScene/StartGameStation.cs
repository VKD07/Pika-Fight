using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameStation : MonoBehaviour
{
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] Slider startGameSlider;
    [SerializeField] TextMeshProUGUI numofPlayersTxt;
    [SerializeField] Image transitionimage;
    [SerializeField] float timeToStart = 5f;
    [SerializeField] float transitionTime = 3f;
    [SerializeField] UnityEvent OnPlayerEntered;
    [SerializeField] UnityEvent OnStartGame;
    Animator anim;
    bool startTransition;
    float currentTime;
    float currentTransitionTime;
    float numOfPlayersJoined;
    int numOfPlayersInTheArea;
    private void Start()
    {
        anim = GetComponent<Animator>();
        numOfPlayersJoined = playerJoinedData.GetNumberOfPlayersJoined();
        numofPlayersTxt.SetText($"{numOfPlayersInTheArea}/{numOfPlayersJoined}");
        startGameSlider.maxValue = timeToStart;
    }

    private void Update()
    {
        if (!startTransition)
        {
            Timer();
        }
        else
        {
            TransitionTimer();
        }
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
            startTransition = true;
        }
    }

    void TransitionTimer()
    {
        if(currentTransitionTime < transitionTime)
        {
            currentTransitionTime += Time.deltaTime;
            Color color = new Color(transitionimage.color.r, transitionimage.color.g, transitionimage.color.b, currentTransitionTime);
            transitionimage.color = color;
        }
        else
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
            OnPlayerEntered.Invoke();
            anim.SetTrigger("PlayerEntered");
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
