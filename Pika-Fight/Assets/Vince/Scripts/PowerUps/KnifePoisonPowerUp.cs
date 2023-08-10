using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePoisonPowerUp : MonoBehaviour
{
    [SerializeField] float poisonDuration = 5f;
    [SerializeField] float poisonDamage = 5f;
    [SerializeField] float attackInterval = 3f;
    [SerializeField] GameObject pickUpVfx;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PoisonousKnife poisonousKnife = other.GetComponentInChildren<PoisonousKnife>();
            poisonousKnife.PoisonDuration = poisonDuration;
            poisonousKnife.PoisonDamage = poisonDamage;
            poisonousKnife.AttackInterval = attackInterval;
            poisonousKnife.enabled = true;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void PickUpVfx()
    {
        GameObject vfx = Instantiate(pickUpVfx, transform.position, Quaternion.identity);
        Destroy(vfx, 0.5f);
    }
}
