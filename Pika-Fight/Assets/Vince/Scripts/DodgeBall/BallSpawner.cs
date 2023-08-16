using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject dodgeBall;
    private void OnEnable()
    {
        StartCoroutine(EnableDodgeBall());
    }

    IEnumerator EnableDodgeBall()
    {
        yield return new WaitForSeconds(2);
        dodgeBall.SetActive(true);
    }
}
