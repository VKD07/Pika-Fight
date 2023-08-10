using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ExplodingCrate : MonoBehaviour
{
    [SerializeField] GameObject explodingVfx;
    [SerializeField] float explosionDamage = 100f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float timeToExplode = 2f;
    [SerializeField] LayerMask layersAffected;
    [SerializeField] float explosionForce = 20f;
    [SerializeField] UnityEvent OnIgnite;
    [SerializeField] UnityEvent OnExplosion;
    Animator anim;
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if(collision.gameObject.GetComponent<Ball>().GetBallDamage > 10)
            {
                OnIgnite.Invoke();
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
                objs.GetComponent<PlayerMovement>().enabled = false;
            }

            if(objs.GetComponent<beeAttack>() != null)
            {
                Destroy(objs);
            }
            ExplosionForce(objs.gameObject);
        }
        OnExplosion.Invoke();
        ExplosionVFX();
        gameObject.SetActive(false);
    }

    void ExplosionForce(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().AddForce((obj.transform.position -transform.position).normalized * explosionForce, ForceMode.Impulse);
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
