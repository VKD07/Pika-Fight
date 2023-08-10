using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReceiveDamage : MonoBehaviour
{
    [SerializeField] FloatReference playerHealth;
    [SerializeField] GameObject shield;
    [SerializeField] UnityEvent OnImpact;
    float initHealth;

    private void Start()
    {
        initHealth = playerHealth.Value;
    }

    public void GetDamage(float damage)
    {
        if(this.enabled && !shield.activeSelf)
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
        if(collision.gameObject.tag == "Ball")
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

    public FloatReference PlayerHealth { set =>  playerHealth = value; }
}
