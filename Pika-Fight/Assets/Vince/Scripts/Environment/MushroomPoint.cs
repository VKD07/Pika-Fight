using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MushroomPoint : MonoBehaviour
{
    [SerializeField] GameObject poisonCloud;
    [SerializeField] UnityEvent OnPoisonReleased;
    Animator anim;
    float damage;
    float damageInterval;
    float poisonDuration;
    bool poisonActivated;
    public float Damage { set => damage = value; }
    public float DamageInterval { set => damageInterval = value; }
    public float PoisonDuration { set => poisonDuration = value; }

    GameObject player;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerPoison()
    {
        anim.SetBool("ReleasePoison", true);
    }

    public void ReleasePoison()
    {
        OnPoisonReleased.Invoke();
        poisonCloud.SetActive(true);
        poisonActivated = true;
        StartCoroutine(DisablePoison());
    }

    IEnumerator DisablePoison()
    {
        yield return new WaitForSeconds(poisonDuration);
        anim.SetBool("ReleasePoison", false);
        poisonCloud.SetActive(false);
        poisonActivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && poisonActivated)
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
        while (player != null && poisonActivated)
        {
            player.GetComponent<ReceiveDamage>().GetDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
