using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthBarUI;
    [SerializeField] Transform healthBarLocation;
    [SerializeField] FloatReference playerHealth;
    Slider healthBarSlider;
    GameObject healthBar;
    void Start()
    {
        InitHealthBar();
    }

    private void OnEnable()
    {
        if (healthBar != null)
        {
            healthBar.SetActive(true);
        }
    }

    private void InitHealthBar()
    {
        healthBar = Instantiate(healthBarUI);
        healthBar.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
        healthBar.name = playerHealth.name;
        healthBarSlider = healthBar.GetComponent<Slider>();
        healthBarSlider.maxValue = playerHealth.Value;
        healthBarSlider.value = playerHealth.Value;
    }

    void Update()
    {
        if(healthBar != null)
        {
            SetHealthBarPos();
            UpdatePlayerHealthUI();
        }
    }

    private void SetHealthBarPos()
    {
        healthBar.transform.position = healthBarLocation.position;
    }

    void UpdatePlayerHealthUI()
    {
        healthBarSlider.value = playerHealth.Value;

        if (playerHealth.Value <= 0)
        {
            healthBar.SetActive(false);
        }
    }

    private void OnDisable()
    {
        healthBarSlider.value = playerHealth.Value;
    }

    public float healthValue
    {
        get { return playerHealth.Value; }
        set { playerHealth.Value = value; }
    }
    public FloatReference PlayerHealth { set { playerHealth = value; } }
}
