using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalRankingUIManager : MonoBehaviour
{
    [SerializeField] Image playerWinnerImg;
    [SerializeField] Image winTxt;
    [SerializeField] Sprite[] playerWinners;
    [SerializeField] Animator canvasAnimation;
    [SerializeField] float animStartDelay = 3;
    [SerializeField] PlayerJoinedData playerJoinedData;
    public float numberOfPlayersJoined;

    void Start()
    {
        SetUpPlayerWinnerImg();
        StartCoroutine(SlideAnimDelay());
    }
    IEnumerator SlideAnimDelay()
    {
        yield return new WaitForSeconds(animStartDelay);
        canvasAnimation.SetTrigger("StartSlide");
    }

    void SetUpPlayerWinnerImg()
    {
        numberOfPlayersJoined = playerJoinedData.NumberOfPlayersJoined;
        for (int i = 0; i < numberOfPlayersJoined; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i].PlayerScore >= 6)
            {
                playerWinnerImg.sprite = playerWinners[i];
                print(playerJoinedData.GetPlayersJoined[i].name);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
