using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] float time = 5f, damage = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ReceiveDamage>())
        {
            ReceiveDamage health = other.GetComponent<ReceiveDamage>();
            health.GetDamage(damage);
        } 
    }
    IEnumerator TakeDamage(float time, ReceiveDamage health)
    {
        health.GetDamage(damage);
        yield return new WaitForSeconds(time);
    }

}
