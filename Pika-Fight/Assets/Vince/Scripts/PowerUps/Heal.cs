
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] GameObject pickUpVfx;
    [SerializeField] float healValue = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealThePlayer(other.GetComponent<HealthBar>());
            PickUpVfx();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    void HealThePlayer(HealthBar playerHealth)
    {
        float totalhealth = playerHealth.healthValue + healValue;
        if (totalhealth < 100)
        {
            playerHealth.healthValue += healValue;
        }
        else
        {
            playerHealth.healthValue = 100;
        }
    }

    private void PickUpVfx()
    {
        GameObject vfx = Instantiate(pickUpVfx, transform.position, Quaternion.identity);
        Destroy(vfx, 0.5f);
    }
}
