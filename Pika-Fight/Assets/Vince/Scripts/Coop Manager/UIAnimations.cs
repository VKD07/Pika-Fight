using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    [SerializeField] GameObject []slots;
    [SerializeField] float toLocation = -1f;
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            LeanTween.moveLocalZ(slots[i], toLocation, .7f).setLoopPingPong();
        }
    }
}
