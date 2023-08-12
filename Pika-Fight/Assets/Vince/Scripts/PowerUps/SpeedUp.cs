using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] GameObject pickUpVfx;
    [SerializeField] float speedUpDuration = 6f;
    [SerializeField] float additionalSpeed = 3f;
    float initSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<TrailRenderer>().enabled = true;
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            initSpeed = playerMovement.PlayerMovementSpeed.Value;
            playerMovement.PlayerMovementSpeed.Value += additionalSpeed;
            playerMovement.DisableSpeedUp(speedUpDuration);
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
