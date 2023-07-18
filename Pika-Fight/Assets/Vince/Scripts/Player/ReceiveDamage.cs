using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReceiveDamage : MonoBehaviour
{
    [SerializeField] FloatReference playerHealth;
    [SerializeField] UnityEvent OnImpact;
    float initHealth;

    private void Start()
    {
        initHealth = playerHealth.Value;
    }

    void GetDamage(float damage)
    {
        playerHealth.Value -= damage;
    }

    private void OnDisable()
    {
        playerHealth.Value = initHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Ball ballScript = collision.gameObject.GetComponent<Ball>();
            OnImpact.Invoke();

            if(ballScript != null)
            {
                GetDamage(ballScript.GetBallDamage);
            }
        }
    }
}
