using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVfx : MonoBehaviour
{
    [SerializeField] ObjectPooling dashVfxPool;
    [SerializeField] Transform dashVfxSpawnPoint;
    Transform gameParticlesParent;

    void Start()
    {
        gameParticlesParent = GameObject.FindGameObjectWithTag("GameParticles").transform;
        dashVfxPool.SetParent(gameParticlesParent);
        dashVfxPool.InitPoolOfObjects();
    }

    public void SpawnDashFx()
    {
        dashVfxPool.PickObjFromPool(dashVfxSpawnPoint);
        dashVfxPool.GetPickedObj.transform.forward = -transform.forward;
        StartCoroutine(DisableObj());
    }


    private void OnDestroy()
    {
        dashVfxPool.ClearList();
    }

    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(1f);
        dashVfxPool.GetPickedObj.SetActive(false);
    }
}
