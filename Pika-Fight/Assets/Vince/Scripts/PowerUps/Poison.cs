using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] GameObject poisonVfx;
    ReceiveDamage receiveDamage;
    float attackInterval;
    float poisonDuration;
    float poisonDamage;
    bool poisonIsActivated;
    private void OnEnable()
    {
        poisonVfx.SetActive(true);
        receiveDamage = GetComponentInParent<ReceiveDamage>();
        poisonIsActivated = true;
        StartCoroutine(ApplyPoision());
        StartCoroutine(DisablePoison());
    }
    IEnumerator ApplyPoision()
    {
        while (poisonIsActivated)
        {
            receiveDamage.GetDamage(poisonDamage);
            yield return new WaitForSeconds(attackInterval);
        }
    }

    IEnumerator DisablePoison()
    {
        yield return new WaitForSeconds(poisonDuration);
        poisonIsActivated = false;
        poisonVfx.SetActive(false);
        this.enabled = false;
    }
    public float PoisonDuration { set => poisonDuration = value; }
    public float PoisonDamage { set => poisonDamage = value; }
    public float AttackInterval { set => attackInterval = value; }
}
