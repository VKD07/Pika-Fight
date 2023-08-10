using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{
    [SerializeField] GameObject pickUpVfx;
    [SerializeField] UnityEvent OnPickup;
    [SerializeField] UnityEvent OnEnableObj;
    [SerializeField] float gemValue = 1;
    private void OnEnable()
    {
       OnEnableObj.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject player = other.gameObject;

            if (player.GetComponentInChildren<GemScoreUI>() != null && !player.GetComponentInChildren<PlayerConfigBridge>().PlayerConfig.PlayerIsDead)
            {
                var vfx = Instantiate(pickUpVfx, transform.position, Quaternion.identity);
                OnPickup.Invoke();
                player.GetComponentInChildren<GemScoreUI>().AddScore(gemValue);
                Destroy(vfx, 1);
                gameObject.SetActive(false);
            }
        }
    }
    public float GemValue { get => gemValue; set => gemValue = value; }
}
