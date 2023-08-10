using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RandomizationEffect : MonoBehaviour
{
    public enum Category
    {
        Combat, Goal, Environment
    }

    [SerializeField] Category category;
    [SerializeField] Image mainImage;
    [SerializeField] Sprite[] images;
    [SerializeField] float randomInterval = 1f;
    [SerializeField] float chooseDelaytime = 5f;
    [SerializeField] GameRandomizer gameRandomizer;
    [SerializeField] UnityEvent OnChoosing;
    [SerializeField] UnityEvent finishedChoosing;
    bool startChoosing;
    void Start()
    {
        StartCoroutine(RandomizeEffect());
        StartCoroutine(StartChoosing());
    }

    IEnumerator RandomizeEffect()
    {
        while (!startChoosing)
        {
            int randomImage = Random.Range(0, images.Length);
            mainImage.sprite = images[randomImage];
            yield return new WaitForSeconds(randomInterval);
            //OnChoosing.Invoke();

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
                mainImage.sprite = images[gameRandomizer.randomCombat];
                break;
            case Category.Goal:
                mainImage.sprite = images[gameRandomizer.randomGoal];
                break;
            case Category.Environment:
                mainImage.sprite = images[gameRandomizer.randomScenes];
                break;
        }
    }
}
