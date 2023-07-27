using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerConfigBridge))]
public class DropGems : MonoBehaviour
{
    [SerializeField] GameObject gemPrefab;
    PlayerConfigBridge playerConfigBridge;
    Vector3 deathpoint;
    bool deathPointMarked;
    private void Start()
    {
        playerConfigBridge = GetComponent<PlayerConfigBridge>();
    }

    public void DropGem()
    {
        if (this.enabled && playerConfigBridge.PlayerConfig.GemScore > 0)
        {
            deathpoint = transform.position;
            StartCoroutine(DropWithDelay());
        }
    }

    IEnumerator DropWithDelay()
    {
        yield return new WaitForSeconds(1.9f);
        var gem = Instantiate(gemPrefab, deathpoint + Vector3.up, Quaternion.Euler(-90f, 0, 0));
        gem.GetComponent<Gem>().GemValue = playerConfigBridge.PlayerConfig.GemScore;
    }
}
