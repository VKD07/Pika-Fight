using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] ObjectPooling vfxPool;
    [SerializeField] Transform vfxSpawnPoint;
    Transform gameParticlesParent;

    void Start()
    {
        gameParticlesParent = GameObject.FindGameObjectWithTag("GameParticles").transform;
        vfxPool.SetParent(gameParticlesParent);
        vfxPool.InitPoolOfObjects();
    }

    public void SpawnFx()
    {
        vfxPool.PickObjFromPool(vfxSpawnPoint);
        StartCoroutine(DisableObj());
    }

    private void OnDisable()
    {
        vfxPool.ClearList();
    }

    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(1f);
        vfxPool.GetPickedObj.SetActive(false);
    }
}