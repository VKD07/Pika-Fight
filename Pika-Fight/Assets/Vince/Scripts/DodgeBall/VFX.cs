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
        vfxPool.InitPoolOfObjects(Quaternion.identity);
    }

    public void SpawnFx()
    {
        vfxPool.PickObjFromPool(vfxSpawnPoint);
    }

    private void OnDisable()
    {
        vfxPool.ClearList();
    }
}
