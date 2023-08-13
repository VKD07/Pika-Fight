using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] float initSize;
    [SerializeField] float maxSize;
    [SerializeField] bool autoMaticLoop;
    [SerializeField] float loopInterval = 0.5f;
    Vector3 initSizeVector;
    Vector3 maxSizeVector;
    private void OnEnable()
    {
        initSizeVector = new Vector3(initSize, initSize, initSize);
        maxSizeVector = new Vector3 (maxSize, maxSize, maxSize);

        if (autoMaticLoop)
        {
            StartCoroutine(LoopAnimation());
        }
    }


    IEnumerator LoopAnimation()
    {
        while (true)
        {
            ZoomInOutAnimation();
            yield return new WaitForSeconds(loopInterval);
        }
    }

    public void ZoomInOutAnimation()
    {
        LeanTween.scale(gameObject, maxSizeVector, 0.09f).setEaseInBack();
        LeanTween.scale(gameObject, initSizeVector, 0.09f).setEaseInBack().setDelay(0.09f);
    }
}
