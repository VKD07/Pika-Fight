using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousKnife : MonoBehaviour
{
    [SerializeField] GameObject poisonousKnifeVfx;
    public float poisonDuration;
    public float poisonDamage;
    public float attackInterval;

    private void OnEnable()
    {
        poisonousKnifeVfx.SetActive(true);
    }

    public void GivePoison(GameObject targetPlayer)
    {
        if(this.enabled)
        {
            Poison poison = targetPlayer.GetComponentInChildren<Poison>();
            poison.AttackInterval = attackInterval;
            poison.PoisonDuration = poisonDuration;
            poison.PoisonDamage = poisonDamage;
            print("Poisoned" + targetPlayer.name);
            poison.enabled = true;
        }
    }
    
    public void DisablePoisonKnife()
    {
        poisonousKnifeVfx.SetActive(false);
        this.enabled = false;
    }

    public float PoisonDuration { set => poisonDuration = value; }
    public float PoisonDamage { set => poisonDamage = value; }
    public float AttackInterval { set => attackInterval = value; }
}
