using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayCountdown : MonoBehaviour
{
    [SerializeField] Image countdownImage;
    [SerializeField] Sprite[] countdownSprites;
    [SerializeField] float timeInterval = 2f;
    [SerializeField] GameObject[] players;
    [SerializeField] float initScale = 0.2216302f;
    [SerializeField] float toScale = 0.3665984f;
    [SerializeField] BoolReference versusScreenPlayed;
    [SerializeField] GameObject versusScreen;
    int imgIndex;
    private void Start()
    {
        countdownImage.enabled = false;
        StartCoroutine(CheckIfCountdownShouldBeEnabled());
    }

    IEnumerator CheckIfCountdownShouldBeEnabled()
    {
        yield return new WaitForSeconds(0.3f);
        if (versusScreenPlayed && !versusScreen.activeSelf)
        {
            countdownImage.enabled = true;
            TextAnimation(toScale);
            players = GameObject.FindGameObjectsWithTag("Player");
            PlayerMovement(false);
            StartCoroutine(Count());
        }
    }

    IEnumerator Count()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            if (imgIndex == 3)
            {
                PlayerMovement(true);
                this.gameObject.SetActive(false);
                break;
            }
            else
            {
                imgIndex++;
                countdownImage.sprite = countdownSprites[imgIndex];
                TextAnimation(toScale);
            }
        }
    }

    void PlayerMovement(bool value)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMovement>().enabled = value;
        }
    }

    void TextAnimation(float toScale)
    {
        LeanTween.scale(countdownImage.gameObject, new Vector3(toScale, toScale, toScale), .09f).setEaseInOutBack();
        LeanTween.scale(countdownImage.gameObject, new Vector3(initScale, initScale, initScale), .09f).setDelay(0.1f).setEase(LeanTweenType.easeInBack);
    }
}
