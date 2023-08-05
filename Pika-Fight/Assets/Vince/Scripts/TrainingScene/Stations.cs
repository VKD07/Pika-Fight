using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stations : MonoBehaviour
{
    [SerializeField] StationType stationType;
    [SerializeField] GameObject interactVfx;
    public enum StationType
    {
        dodgeball,
        melee
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (stationType == StationType.dodgeball)
            {
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

    void SpawnVfx(Transform spawnPos)
    {
        GameObject vfx = Instantiate(interactVfx, spawnPos.position, Quaternion.identity);
        Destroy(vfx,1f);
    }
}
