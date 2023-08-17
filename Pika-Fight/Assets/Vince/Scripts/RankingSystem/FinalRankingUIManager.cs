using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalRankingUIManager : MonoBehaviour
{
    [SerializeField] Image playerWinnerImg;
    [SerializeField] Image winTxt;
    [SerializeField] Sprite[] playerWinners;
    [SerializeField] Animator canvasAnimation;
    [SerializeField] float animStartDelay = 3;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [Header("Head To Lobby")]
    [SerializeField] KeyCode proceedBtn;
    [SerializeField] string lobbySceneName;
    
    [SerializeField] UnityEvent OnTxtShown;
    public float numberOfPlayersJoined;

    void Start()
    {
        SetUpPlayerWinnerImg();
        StartCoroutine(SlideAnimDelay());
    }

    private void Update()
    {
        ProceedToLobby();
    }

    private void ProceedToLobby()
    {
        if (Input.GetKeyDown(proceedBtn))
        {
            SceneManager.LoadScene(lobbySceneName);
        }
    }

    IEnumerator SlideAnimDelay()
    {
        yield return new WaitForSeconds(animStartDelay);
        canvasAnimation.SetTrigger("StartSlide");
        OnTxtShown.Invoke();
    }

    void SetUpPlayerWinnerImg()
    {
        numberOfPlayersJoined = playerJoinedData.NumberOfPlayersJoined;
        for (int i = 0; i < numberOfPlayersJoined; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i].PlayerScore >= 6)
            {
                playerWinnerImg.sprite = playerWinners[i];
                proceedBtn = playerJoinedData.GetPlayersJoined[i].Player_Controls.PlayerStartKey;
                break;
            }
        }
    }
}
