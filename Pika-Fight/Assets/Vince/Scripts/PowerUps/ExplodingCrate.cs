using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodingCrate : MonoBehaviour
{
    [SerializeField] GameObject explodingVfx;
    [SerializeField] float explosionDamage = 100f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float timeToExplode = 2f;
    [SerializeField] LayerMask layersAffected;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if(collision.gameObject.GetComponent<Ball>().GetBallDamage > 10)
            {
                anim.SetTrigger("Explode");
                StartCoroutine(Explode());
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponentInChildren<MeleeFight>().Stabbing)
            {
                anim.SetTrigger("Explode");
                StartCoroutine(Explode());
            }
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeToExplode);

        Collider[] objsDetected = Physics.OverlapSphere(transform.position, explosionRadius, layersAffected);
        foreach (Collider objs in objsDetected)
        {
            if(objs.GetComponent<ReceiveDamage>() != null)
            {
                objs.GetComponent<ReceiveDamage>().GetDamage(explosionDamage);
            }

            objs.GetComponent<Rigidbody>().AddForce(-objs.transform.forward * 20f, ForceMode.Impulse);
        }
        ExplosionVFX();
        Destroy(gameObject);
    }
    
    void ExplosionVFX()
    {
        GameObject vfx = Instantiate(explodingVfx, transform.position, Quaternion.identity);
        vfx.transform.localScale = Vector3.one * 2f;
        Destroy(vfx, 1f);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, explosionRadius);
    //}
}
