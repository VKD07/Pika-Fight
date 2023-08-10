using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPoint : MonoBehaviour
{
    float damage;
    float damageInterval;
    public float Damage { set => damage = value; }
    public float DamageInterval { set => damageInterval = value; }
    GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            StartCoroutine(DamagePlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }

    IEnumerator DamagePlayer()
    {
        while (player != null)
        {
            if (player != null)
            {
                player.GetComponent<ReceiveDamage>().GetDamage(damage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
