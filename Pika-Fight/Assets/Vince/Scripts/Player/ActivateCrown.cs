using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCrown : MonoBehaviour
{
    [SerializeField] GameObject crown;
    public void ActivateTheCrown(bool enable)
    {
        crown.SetActive(enable);
    }
}
