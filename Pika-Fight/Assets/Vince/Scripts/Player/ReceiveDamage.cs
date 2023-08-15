using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ReceiveDamage : MonoBehaviour
{
    [SerializeField] FloatReference startingHealth;
    [SerializeField] FloatReference playerHealth;
    [SerializeField] GameObject shield;
    [SerializeField] UnityEvent OnImpact;

    private void Start()
    {
        startingHealth.Value = playerHealth.Value;
    }

    public void GetDamage(float damage)
    {
        if (this.enabled && !shield.activeSelf)
        {
            playerHealth.Value -= damage;
        }
        OnImpact.Invoke();
    }


    private void OnDisable()
    {
        // playerHealth.Value = initHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Ball ballScript = collision.gameObject.GetComponent<Ball>();
        }
    }

    public void DeactivateShield(float shieldDuration)
    {
        StartCoroutine(DisableShield(shieldDuration));
    }

    IEnumerator DisableShield(float shieldDuration)
    {
        yield return new WaitForSeconds(shieldDuration);
        shield.SetActive(false);
    }

    public FloatReference PlayerHealth { set => playerHealth = value; }
    public GameObject Shield => shield;
}
