using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGems : MonoBehaviour
{
    [SerializeField] GameObject gemPrefab;
    PlayerConfigBridge playerConfigBridge;
    private void Start()
    {
        playerConfigBridge = GetComponentInParent<PlayerConfigBridge>();
    }

    public void DropGem()
    {
        if (this.enabled && playerConfigBridge.PlayerConfig.GemScore > 0)
        {
            StartCoroutine(DropWithDelay());
        }
    }

    IEnumerator DropWithDelay()
    {
        yield return new WaitForSeconds(1.9f);
        var gem = Instantiate(gemPrefab, transform.position + Vector3.up, Quaternion.Euler(-90f, 0, 0));
        gem.GetComponent<Gem>().GemValue = playerConfigBridge.PlayerConfig.GemScore;
    }
}
