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

    private void InitHealthBar()
    {
        healthBar = Instantiate(healthBarUI);
        healthBar.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
        healthBarSlider = healthBar.GetComponent<Slider>();
        healthBarSlider.maxValue = playerHealth.Value;
        healthBarSlider.value = playerHealth.Value;
    }

    // Update is called once per frame
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
            Destroy(healthBar);
        }
    }

    public float healthValue
    {
        set { playerHealth.Value = value; }
    }
}
