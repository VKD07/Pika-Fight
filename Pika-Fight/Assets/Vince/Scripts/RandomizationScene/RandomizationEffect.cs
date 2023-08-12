using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class RandomizationEffect : MonoBehaviour
{
    public enum Category
    {
        Combat, Goal, Environment
    }

    [SerializeField] Category category;
    [Header("For Videos ========")]
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip[] clips;
    [SerializeField] GameObject texture;
    [Header("For Random Images animation ========")]
    [SerializeField] Image mainImage;
    [SerializeField] Sprite[] randomImages;
    [Header("For Category Text ========")]
    [SerializeField] Image categoryTitleImg;
    [SerializeField] Sprite[] categoryTitles;
    [Header("For Environment Category ========")]
    [SerializeField] Image environmentImage;
    [SerializeField] Sprite [] environments;
    [Header("Randomization Settings ========")]
    [SerializeField] float randomInterval = 1f;
    [SerializeField] float chooseDelaytime = 5f;
    [SerializeField] GameRandomizer gameRandomizer;
    [Header("Events ========")]
    [SerializeField] UnityEvent OnChoosing;
    [SerializeField] UnityEvent finishedChoosing;
    bool startChoosing;
    void Start()
    {
        OnChoosing.Invoke();
        StartCoroutine(RandomizeEffect());
        StartCoroutine(StartChoosing());
    }

    IEnumerator RandomizeEffect()
    {
        while (!startChoosing)
        {
            int randomImage = Random.Range(0, randomImages.Length);
            mainImage.sprite = randomImages[randomImage];
            yield return new WaitForSeconds(randomInterval);
        }
    }

    IEnumerator StartChoosing()
    {
        yield return new WaitForSeconds(chooseDelaytime);
        startChoosing = true;
        SelectTheRightImage();
        finishedChoosing.Invoke();
    }

    void SelectTheRightImage()
    {
        switch (category)
        {
            case Category.Combat:
                mainImage.enabled = false;
                texture.SetActive(true);
                videoPlayer.clip = clips[gameRandomizer.randomCombat];
                categoryTitleImg.sprite = categoryTitles[gameRandomizer.randomCombat];
                break;
            case Category.Goal:
                mainImage.enabled = false;
                texture.SetActive(true);
                videoPlayer.clip = clips[gameRandomizer.randomGoal];
                categoryTitleImg.sprite = categoryTitles[gameRandomizer.randomGoal];
                break;
            case Category.Environment:
                mainImage.enabled = false;
                texture.SetActive(true);
                environmentImage.sprite = environments[gameRandomizer.randomScenes];
                categoryTitleImg.sprite = categoryTitles[gameRandomizer.randomScenes];
                break;
        }
    }
}
