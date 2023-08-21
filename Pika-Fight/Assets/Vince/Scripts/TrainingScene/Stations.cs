using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stations : MonoBehaviour
{
    [SerializeField] StationType stationType;
    [SerializeField] GameObject interactVfx;
    [SerializeField] Transform ballSpawner;
    [SerializeField] GameObject dodgeBallPrefab;
    [SerializeField] UnityEvent OnPlayerEnter;
    public enum StationType
    {
        dodgeball,
        melee
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnPlayerEnter.Invoke();
            if (stationType == StationType.dodgeball)
            {
                SpawnBall();
                other.GetComponentInChildren<DodgeBall>().enabled = true;
                other.GetComponentInChildren<MeleeFight>().enabled = false;
            }
            else if(stationType == StationType.melee)
            {
                other.GetComponentInChildren<DodgeBall>().enabled = false;
                other.GetComponentInChildren<MeleeFight>().enabled = true;
            }
            SpawnVfx(other.transform);
        }
    }

    void SpawnBall()
    {
        Instantiate(dodgeBallPrefab, ballSpawner.position, Quaternion.identity);
    }

    void SpawnVfx(Transform spawnPos)
    {
        GameObject vfx = Instantiate(interactVfx, spawnPos.position, Quaternion.identity);
        Destroy(vfx,1f);
    }
}
