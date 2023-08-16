using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeFight : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] GameObject knifeWeapon;
    [SerializeField] FloatReference weaponDamage;
    [SerializeField] FloatReference weaponAttackRange;
    [SerializeField] public bool playerDetected;
    [SerializeField] Transform weaponRangeOrigin;
    [SerializeField] LayerMask layerToStab;
    [SerializeField] PoisonousKnife poisonousKnife;
    [SerializeField] ObjectSpawner swordSparkVfx;

    [Header("Script references")]
    [SerializeField] PlayerControls playerControls;
    PlayerConfigBridge playerConfigBridge;

    [Header("Dashing")]
    Rigidbody rb;
    [SerializeField] FloatReference dashForce;
    [SerializeField] FloatReference dashTime;
    [SerializeField] FloatReference attackCooldown;
    [SerializeField] UnityEvent IsDashing;
    [SerializeField] UnityEvent IsNotDashing;
    [SerializeField] UnityEvent OnStab;
    [SerializeField] UnityEvent OnPlayerImpact;
    [SerializeField] UnityEvent OnSwordCollide;
    public bool isDashing;
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

        players = Physics.OverlapSphere(weaponRangeOrigin.position, weaponAttackRange.Value, layerToStab);
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
                    if (players[i].GetComponentInChildren<MeleeFight>().isDashing)
                    {
                        swordSparkVfx.InstantiateObj(knifeWeapon.transform, Quaternion.identity);
                        rb.velocity = Vector3.zero;
                        players[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        OnSwordCollide.Invoke();
                    }
                    else
                    {
                        ChickenMode(players[i].gameObject, weaponDamage.Value);
                        players[i].GetComponent<ReceiveDamage>().GetDamage(weaponDamage.Value);
                    }
                    OnPlayerImpact.Invoke();
                    ApplyPoison(players[0].gameObject);
                }
            }
            BeginDash();
            OnStab.Invoke();
        }
    }
    private void BeginDash()
    {
        IsDashing.Invoke();
        rb.velocity = transform.forward * dashForce.Value;
        isDashing = true;
        StartCoroutine(StopDash());
        StartCoroutine(EnableDash());
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashTime.Value);
        IsNotDashing.Invoke();
        rb.velocity = Vector3.zero;
    }

    IEnumerator EnableDash()
    {
        yield return new WaitForSeconds(attackCooldown.Value);
        isDashing = false;
    }

    public void GiveDamage()
    {
        if (playerDetected)
        {
            hit.collider.GetComponent<ReceiveDamage>().GetDamage(weaponDamage.Value);
        }
    }

    void ChickenMode(GameObject player, float damageDealt)
    {
        if (player.GetComponentInChildren<ChickenMode>().enabled)
        {
            playerConfigBridge.PlayerConfig.DamageDealtToChicken += damageDealt;
        }
    }

    void ApplyPoison(GameObject targetPlayer)
    {
        if (poisonousKnife.enabled)
        {
            poisonousKnife.GivePoison(targetPlayer);
            poisonousKnife.DisablePoisonKnife();
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //Gizmos.DrawRay(weaponRangeOrigin.position, weaponRangeOrigin.forward * weaponAttackRange);
    //    Gizmos.DrawWireSphere(weaponRangeOrigin.position, weaponAttackRange.Value);
    //}

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
    public bool IStunned { set => isStunned = value; }
    public bool Stabbing => isDashing;
    public float AttackCoolDown { get => attackCooldown.Value; set => attackCooldown.Value = value; }
}
