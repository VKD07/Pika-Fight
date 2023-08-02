using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLoopDelay : MonoBehaviour
{
    public GameObject Fire;
    public float DelayTimer;
    public float FireDuration;
    public bool FireIsWorking;
    public float Damage;

    void Start()
    {
        StartCoroutine(StartFire());
    }

    void Update()
    {

    }

    IEnumerator StartFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(DelayTimer);
            Fire.SetActive(true);
            FireIsWorking = true;
            yield return StartCoroutine(FireIsActivated()); // Wait for FireIsActivated coroutine to finish
        }
    }

    IEnumerator FireIsActivated()
    {
        yield return new WaitForSeconds(FireDuration);
        Fire.SetActive(false);
        FireIsWorking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("PlayerIsDetected");
            other.GetComponent<ReceiveDamage>().GetDamage(Damage);
        }
    }



}
