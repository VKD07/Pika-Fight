using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    [SerializeField] GameObject []slots;
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            LeanTween.moveLocalZ(slots[i], -1f, .7f).setLoopPingPong();
        }
    }
}
