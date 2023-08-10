using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
    [SerializeField] bool ballTaken;
    [SerializeField] float ballDamage;
    [SerializeField] float ballForce = 10f;
    [Header("Raycast")]
    [SerializeField] float rayDistance = 1f;
    [SerializeField] LayerMask playerLayer;
    RaycastHit hit;

    [Header("Exploding Ball")]
    [SerializeField] bool exploding;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionDamage = 50f;
    [SerializeField] float explosionForce = 15f;
    [SerializeField] GameObject explodingVfx;
    [SerializeField] UnityEvent OnExplosion;

    [Header("Chicken Ball")]
    [SerializeField] GameObject chickenTransformVfx;
    [SerializeField] bool chickenBall;
    [SerializeField] float chickenDebuffDuration = 5f;

    Beehive beehive;

    bool exploded;
    [Header("Events")]
    [SerializeField] UnityEvent OnImpact;
    [SerializeField] UnityEvent OnPlayerImpact;
    Rigidbody rb;
    SphereCollider sphereCollider;
    TrailRenderer trailRenderer;
    //To Collect the damage dealt from previous owner
    [SerializeField] GameObject previousOwner;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        trailRenderer = GetComponent<TrailRenderer>();
        beehive = GetComponent<Beehive>();
    }

    private void Update()
    {
        ActivateTrail();
        SetBallDamage();
        DetectPlayer();
    }

    private void ActivateTrail()
    {
        if (rb.velocity.magnitude > 20)
        {
            trailRenderer.enabled = true;
        }
        else if (rb.velocity.magnitude < 5)
        {
            trailRenderer.enabled = false;
        }
    }

    private void SetBallDamage()
    {
        ballDamage = rb.velocity.magnitude;
    }

    private void DetectPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, playerLayer))
        {
            if (rb.velocity.magnitude > 10 && ballTaken)
            {
                if (exploding)
                {
                    InstantiateExplosion(hit.transform);
                }
                else
                {
                    hit.collider.GetComponent<ReceiveDamage>().GetDamage(ballDamage);
                }
                ChickenBall(hit.collider.gameObject);
                ChickenMode(hit.collider.gameObject, ballDamage);
                OnPlayerImpact.Invoke();
            }
        }
    }

    public void SetSphereTrigger(bool enable)
    {
        if (!enable)
        {
            StartCoroutine(SetSphereTriggerToFalse(false));
        }
        else
        {
            sphereCollider.isTrigger = enable;
        }
    }

    IEnumerator SetSphereTriggerToFalse(bool enable)
    {
        yield return new WaitForSeconds(0.1f);
        sphereCollider.isTrigger = enable;
    }

    public IEnumerator AllowBallToBePicked()
    {
        yield return new WaitForSeconds(2);
        ballTaken = false;
    }

    //for the chicken game mode
    void ChickenMode(GameObject player, float damageDealt)
    {
        if (player.GetComponentInChildren<ChickenMode>().enabled && previousOwner != null)
        {
            previousOwner.GetComponentInParent<PlayerConfigBridge>().PlayerConfig.DamageDealtToChicken += damageDealt;
        }
    }
    // Exploding Dodge Ball
    void InstantiateExplosion(Transform spawnLoc)
    {
        StartCoroutine(explode(spawnLoc));
    }

    IEnumerator explode(Transform spawnLoc)
    {
        yield return new WaitForSeconds(0.1f);

        Collider[] playersDetected = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);

        if (!exploded)
        {
            exploded = true;
            foreach (Collider playersInside in playersDetected)
            {
                playersInside.GetComponent<PlayerMovement>().enabled = false;
                playersInside.GetComponent<ReceiveDamage>().GetDamage(explosionDamage);
                playersInside.GetComponent<Rigidbody>().AddForce((playersInside.transform.position - transform.position).normalized * explosionForce, ForceMode.Impulse);
            }
        }

        OnExplosion.Invoke();
        GameObject explosion = Instantiate(explodingVfx, spawnLoc.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * 2f;
        Destroy(explosion, 1f);
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    // Chicken Ball
    void ChickenBall(GameObject player)
    {
        if (chickenBall)
        {
            ChickenDebuf chickenDebuff = player.GetComponentInChildren<ChickenDebuf>();
            if(chickenDebuff != null)
            {
                chickenDebuff.ChickenDuration = chickenDebuffDuration;
                chickenDebuff.enabled = true;
                GameObject vfx = Instantiate(chickenTransformVfx, transform.position, Quaternion.identity);
                Destroy(vfx, 1f);
            }
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    //beehive

    void Beehive(Transform target)
    {
        if(beehive != null)
        {
            beehive.InstantiateBees(transform, target);
        }
    }

    public bool BallTaken
    {
        get { return ballTaken; }
        set { ballTaken = value; }
    }

    public float GetBallDamage => ballDamage;

    public GameObject PreviousOwner { get => previousOwner; set { previousOwner = value; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > 10)
        {
            OnImpact.Invoke();
        }

        if (collision.gameObject.tag == "Player" && rb.velocity.magnitude > 10 && ballTaken)
        {
            OnPlayerImpact.Invoke();
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().AddForce((transform.position - collision.transform.position).normalized * ballForce, ForceMode.Impulse);
            collision.gameObject.GetComponent<ReceiveDamage>().GetDamage(ballDamage);
            ChickenMode(collision.gameObject, ballDamage);
            ChickenBall(collision.gameObject);
        }

        if (rb.velocity.magnitude > 10 && ballTaken)
        {
            OnImpact.Invoke();
            Beehive(collision.gameObject.transform);
        }

        if (exploding && rb.velocity.magnitude > 10 && ballTaken)
        {
            OnImpact.Invoke();
            InstantiateExplosion(transform);
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, explosionRadius);
    //}
}
