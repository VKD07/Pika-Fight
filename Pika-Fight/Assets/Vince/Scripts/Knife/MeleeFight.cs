using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeFight : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] GameObject knifeWeapon;
    [SerializeField] float weaponDamage = 33f;
    [SerializeField] float weaponAttackRange = 1f;
    [SerializeField] public bool playerDetected;
    [SerializeField] Transform weaponRangeOrigin;
    [SerializeField] LayerMask layerToStab;
    [Header("Script references")]
    [SerializeField] PlayerControls playerControls;
    PlayerConfigBridge playerConfigBridge;

    [Header("Dashing")]
    Rigidbody rb;
    [SerializeField] float dashForce = 10f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float attackCoolDown = 2f;
    [SerializeField] UnityEvent IsDashing;
    [SerializeField] UnityEvent IsNotDashing;
    [SerializeField] UnityEvent OnStab;
    bool isDashing;
    bool isStunned;

    Animator anim;
    Collider[] players;
    RaycastHit hit;
    Ray ray;
    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        playerConfigBridge = GetComponentInParent<PlayerConfigBridge>();
    }
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnEnable()
    {
        knifeWeapon.SetActive(true);
        anim.SetBool("Knife", true);
    }

    private void OnDisable()
    {
        knifeWeapon.SetActive(false);
        anim.SetBool("Knife", false);
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayerEnemy();
        Stab();
    }

    private void DetectPlayerEnemy()
    {
        //ray = new Ray(weaponRangeOrigin.position, weaponRangeOrigin.forward);
        //if (Physics.Raycast(ray, out hit, weaponAttackRange, layerToStab))
        //{
        //    playerDetected = true;
        //}
        //else
        //{
        //    playerDetected = false;
        //}

        players = Physics.OverlapSphere(weaponRangeOrigin.position, weaponAttackRange, layerToStab);
        if (players.Length > 0)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    void Stab()
    {
        if (Input.GetKeyDown(playerControls.GetAttackKey) && !isDashing && !isStunned)
        {
            if (playerDetected)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<ReceiveDamage>().GetDamage(weaponDamage);
                    ChickenMode(players[i].gameObject, weaponDamage);
                }
            }
            BeginDash();
            OnStab.Invoke();
        }
    }
    private void BeginDash()
    {
        IsDashing.Invoke();
        rb.velocity = transform.forward * dashForce;
        isDashing = true;
        StartCoroutine(StopDash());
        StartCoroutine(EnableDash());
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashTime);
        IsNotDashing.Invoke();
        rb.velocity = Vector3.zero;
    }

    IEnumerator EnableDash()
    {
        yield return new WaitForSeconds(attackCoolDown);
        isDashing = false;
    }

    public void GiveDamage()
    {
        if (playerDetected)
        {
            hit.collider.GetComponent<ReceiveDamage>().GetDamage(weaponDamage);
        }
    }

    void ChickenMode(GameObject player, float damageDealt)
    {
        if(player.GetComponentInChildren<ChickenMode>().enabled)
        {
            playerConfigBridge.PlayerConfig.DamageDealtToChicken += damageDealt;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(weaponRangeOrigin.position, weaponRangeOrigin.forward * weaponAttackRange);
        Gizmos.DrawWireSphere(weaponRangeOrigin.position, weaponAttackRange);
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
    public bool IStunned { set => isStunned = value; }
    public bool Stabbing => isDashing;
    public float AttackCoolDown { get => attackCoolDown; set => attackCoolDown = value; }
}
