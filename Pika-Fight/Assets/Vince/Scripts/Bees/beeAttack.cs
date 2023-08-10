using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeAttack : MonoBehaviour
{
    [SerializeField] float beeDamage = 20f;
    [SerializeField] float attackInterval = 4f;
    BeeBoids beeBoids;
    private void Start()
    {
        beeBoids = GetComponent<BeeBoids>();
        StartCoroutine(AttackWithDelay(beeBoids.targetObject.gameObject));
    }

    IEnumerator AttackWithDelay(GameObject target)
    {
        while(true)
        {
            yield return new WaitForSeconds(attackInterval);
            target.GetComponent<ReceiveDamage>().GetDamage(beeDamage);
        }
    }
}
