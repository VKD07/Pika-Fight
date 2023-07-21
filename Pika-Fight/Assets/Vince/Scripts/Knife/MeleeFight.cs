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
    [SerializeField] UnityEvent OnStab;
    Animator anim;
    public bool playerDetected;
    RaycastHit hit;
    Ray ray;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        knifeWeapon.SetActive(true);
        anim.SetBool("Knife", true);
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
        if (Input.GetKeyDown(playerControls.GetAttackKey))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Impulse);
            OnStab.Invoke();
        }
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
}
