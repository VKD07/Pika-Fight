using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoldPercentageUI : MonoBehaviour
{
    [SerializeField] GameObject percentageUIPrefab;
    [SerializeField] Transform UILocation;
    PlayerConfigBridge playerConfigBridge;
    GameObject percentageUI;
    void Start()
    {
        playerConfigBridge = GetComponentInParent<PlayerConfigBridge>();
        InitPercentageValue();
    }
    private void OnEnable()
    {
        if (percentageUI != null)
        {
            percentageUI.SetActive(true);
        }
    }

    private void InitPercentageValue()
    {
        percentageUI = Instantiate(percentageUIPrefab);
        percentageUI.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
        percentageUI.name = playerConfigBridge.PlayerConfig.name + "_Percentage";
        int percentage = (int)playerConfigBridge.PlayerConfig.HoldPercentage;
        percentageUI.GetComponentInChildren<TextMeshProUGUI>().SetText(percentage.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (percentageUI != null)
        {
            SetPercentagePos();
            UpdatePercentageValue();
        }
    }

    private void SetPercentagePos()
    {
        percentageUI.transform.position = UILocation.position;
    }

    void UpdatePercentageValue()
    {
        int percentage = (int)playerConfigBridge.PlayerConfig.HoldPercentage;
        percentageUI.GetComponentInChildren<TextMeshProUGUI>().SetText(percentage.ToString());
    }

    public void AddValue(float value)
    {
        playerConfigBridge.PlayerConfig.HoldPercentage += value;
    }

    private void OnDisable()
    {
        if (percentageUI != null)
        {
            percentageUI.SetActive(false);
        }
    }
}
