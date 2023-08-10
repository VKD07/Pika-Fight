using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour
{
    [SerializeField] GameObject bees;
    [SerializeField] float beesDuration = 10f;
    [SerializeField] float numberOfBees = 4;
    [SerializeField] GameObject vfx;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void InstantiateBees(Transform spawnLoc, Transform target)
    {
        if(target.gameObject.tag == "Player")
        {
            for (int i = 0; i < numberOfBees; i++)
            {
                GameObject bee = Instantiate(bees, spawnLoc.position, Quaternion.identity);
                bee.GetComponent<BeeBoids>().targetObject = target;
                Destroy(bee, beesDuration);
            }
        }
        gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
        Vfx();
    }

    void Vfx()
    {
        GameObject vfxImpact = Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(vfxImpact, 1);
    }
}
