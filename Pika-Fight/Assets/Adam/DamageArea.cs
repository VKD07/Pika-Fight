using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] float damage = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ReceiveDamage>())
        {
            ReceiveDamage health = other.GetComponent<ReceiveDamage>();
            DealDamage(health);
        }
    }
    void DealDamage(ReceiveDamage health)
    {
        health.GetDamage(damage);
    }

}
