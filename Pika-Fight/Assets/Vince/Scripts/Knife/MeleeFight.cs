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
    [SerializeField] Transform weaponRangeOrigin;
    [SerializeField] LayerMask layerToStab;
    [Header("Script references")]
    [SerializeField] PlayerControls playerControls;

    [Header("Dashing")]
    Rigidbody rb;
    [SerializeField] float dashForce = 10f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashCoolDown = 2f;
    [SerializeField] UnityEvent IsDashing;
    [SerializeField] UnityEvent IsNotDashing;
    [SerializeField] UnityEvent OnStab;
    bool isDashing;
    
    Animator anim;
    public bool playerDetected;
    RaycastHit hit;
    Ray ray;
    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
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
        ray = new Ray(weaponRangeOrigin.position, weaponRangeOrigin.forward);

        if (Physics.Raycast(ray, out hit, weaponAttackRange, layerToStab))
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
        if (Input.GetKeyDown(playerControls.GetAttackKey) && !isDashing)
        {
            if (playerDetected)
            {
                hit.collider.GetComponent<ReceiveDamage>().GetDamage(weaponDamage);
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
        yield return new WaitForSeconds(dashCoolDown);
        isDashing = false;
    }

    public void GiveDamage()
    {
        if (playerDetected)
        {
            hit.collider.GetComponent<ReceiveDamage>().GetDamage(weaponDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(weaponRangeOrigin.position, weaponRangeOrigin.forward * weaponAttackRange);
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
}
