using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerJoinCountdown : MonoBehaviour
{
    [SerializeField] Image countdownImage;
    [SerializeField] Sprite[] numbers;
    [SerializeField] float timeInterval = 3f;
    [SerializeField] UnityEvent OnFinishCount;
    [SerializeField] UnityEvent OnCount;
    Vector3 initScale;
    int num;

    private void OnEnable()
    {
        OnCount.Invoke();
        initScale = new Vector3(0.7077497f, 0.7077497f, 0.7077497f);
        PopAnimation(1.104441f);
        countdownImage.sprite = numbers[num];
        StartCoroutine(StartCount());
    }

    private void OnDisable()
    {
        countdownImage.gameObject.transform.localScale = initScale;
        num = 0;
    }

    private void Update()
    {
        if (num >= 3)
        {
            OnFinishCount.Invoke();
        }
        else
        {
            countdownImage.sprite = numbers[num];
        }
    }

    IEnumerator StartCount()
    {
        while (num < 3)
        {
            yield return new WaitForSeconds(timeInterval);
            OnCount.Invoke();
            countdownImage.gameObject.transform.localScale = initScale;
            PopAnimation(1.104441f);
            num++;
        }
    }

    void PopAnimation(float scale = 1.104441f)
    {
        LeanTween.scale(countdownImage.gameObject, new Vector3(scale, scale, scale), .7f).setEaseOutElastic();
    }
}

