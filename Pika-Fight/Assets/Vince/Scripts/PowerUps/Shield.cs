using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SearchService;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject pickUpVfx;
    [SerializeField] float shieldDuration = 6f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject player = other.gameObject;
            player.transform.Find("Shield").gameObject.SetActive(true);
            player.GetComponent<ReceiveDamage>().DeactivateShield(shieldDuration);
            PickUpVfx();
            gameObject.SetActive(false);
        }
    }

    private void PickUpVfx()
    {
        GameObject vfx = Instantiate(pickUpVfx, transform.position, Quaternion.identity);
        Destroy(vfx, 0.5f);
    }
}
