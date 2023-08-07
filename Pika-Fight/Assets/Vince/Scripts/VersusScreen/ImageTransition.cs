using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTransition : MonoBehaviour
{
    [SerializeField] GameObject image, image2, image3, image4;
    void Start()
    {
        LeanTween.moveLocal(image, new Vector3(-776, 33, 0), .7f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(image2, new Vector3(-237.5f, -14f, 0), .7f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(image3, new Vector3(297, -75f, 0), .7f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(image4, new Vector3(784, -40, 0), .7f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
    }
}
