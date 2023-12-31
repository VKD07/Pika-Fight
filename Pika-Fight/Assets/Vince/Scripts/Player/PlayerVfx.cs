using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerVfx : MonoBehaviour
{
    [SerializeField] ObjectPooling dashVfxPool;
    [SerializeField] Transform dashVfxSpawnPoint;
    [SerializeField] GameObject respawnVfx;
    [SerializeField] UnityEvent OnEnableScript;
    Transform gameParticlesParent;

    void Start()
    {
        gameParticlesParent = GameObject.FindGameObjectWithTag("GameParticles").transform;
        dashVfxPool.SetParent(gameParticlesParent);
        dashVfxPool.InitPoolOfObjects(Quaternion.identity);
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

    private void OnEnable()
    {
        OnEnableScript.Invoke();
        respawnVfx.SetActive(true);
    }

    private void OnDisable()
    {
        respawnVfx.SetActive(false);
    }

    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(1f);
        dashVfxPool.GetPickedObj.SetActive(false);
    }
}
