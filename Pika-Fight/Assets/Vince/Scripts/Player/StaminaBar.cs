using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeleeFight))]
public class StaminaBar : MonoBehaviour
{
    [SerializeField] GameObject staminaBarUI;
    [SerializeField] Transform staminaUILocation;
    bool attacked;
    Slider staminaSlider;
    GameObject stamina;
    MeleeFight meleeScript;
    void Start()
    {
        meleeScript = GetComponent<MeleeFight>();
        InitHealthBar();
    }

    private void OnEnable()
    {
        if (stamina != null)
        {
            stamina.SetActive(true);
        }
    }

    private void InitHealthBar()
    {
        stamina = Instantiate(staminaBarUI);
        stamina.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
        stamina.name = gameObject.name;
        staminaSlider = stamina.GetComponent<Slider>();
        staminaSlider.maxValue = meleeScript.AttackCoolDown;
        staminaSlider.value = meleeScript.AttackCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina != null)
        {
            SetStaminaBarPos();
            UpdateStaminaUI();
        }
    }

    private void SetStaminaBarPos()
    {
        stamina.transform.position = staminaUILocation.position;
    }

    void UpdateStaminaUI()
    {
        if (attacked)
        {
            attacked = false;
            staminaSlider.value = 0;
        }

        if(staminaSlider.value < meleeScript.AttackCoolDown)
        {
            staminaSlider.value += Time.deltaTime;
        }
    }

    private void OnDisable()
    {
        if(stamina != null)
        {
            staminaSlider.value = meleeScript.AttackCoolDown;
            stamina.SetActive(false);
        }
    }

    public bool Attacked { set => attacked = value; }
}
