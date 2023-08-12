using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] float initSize;
    [SerializeField] float maxSize;
    Vector3 initSizeVector;
    Vector3 maxSizeVector;
    private void Start()
    {
        initSizeVector = new Vector3(initSize, initSize, initSize);
        maxSizeVector = new Vector3 (maxSize, maxSize, maxSize);
    }
    public void ZoomInOutAnimation()
    {
        LeanTween.scale(gameObject, maxSizeVector, 0.09f).setEaseInBack();
        LeanTween.scale(gameObject, initSizeVector, 0.09f).setEaseInBack().setDelay(0.09f);
    }
}
