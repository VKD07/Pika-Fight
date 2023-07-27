using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{
    [SerializeField] UnityEvent OnPickup;
    [SerializeField] GameObject pickUpVfx;
    [SerializeField] float gemValue = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject player = other.gameObject;

            if (player.GetComponent<GemScoreUI>() != null && !player.GetComponent<PlayerConfigBridge>().PlayerConfig.PlayerIsDead)
            {
                var vfx = Instantiate(pickUpVfx, transform.position, Quaternion.identity);
                OnPickup.Invoke();
                player.GetComponent<GemScoreUI>().AddScore(gemValue);
                Destroy(vfx, 1);
                Destroy(gameObject,1);
            }
        }
    }
    public float GemValue { get => gemValue; set => gemValue = value; }
}
