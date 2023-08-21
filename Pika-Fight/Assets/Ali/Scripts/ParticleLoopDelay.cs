using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleLoopDelay : MonoBehaviour
{
    public GameObject Fire;
    public float DelayTimer;
    public float FireDuration;
    public bool FireIsWorking;
    public float Damage;
    public UnityEvent OnFireActivated;

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
            OnFireActivated.Invoke();
            Fire.SetActive(true);
            FireIsWorking = true;
            yield return StartCoroutine(FireIsActivated());
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
        if (other.tag == "Player" && FireIsWorking)
        {
            other.GetComponent<ReceiveDamage>().GetDamage(Damage);
        }
    }
}
