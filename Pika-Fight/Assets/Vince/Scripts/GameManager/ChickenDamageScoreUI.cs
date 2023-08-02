using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChickenDamageScoreUI : MonoBehaviour
{
    [SerializeField] GameObject chickenDamageScoreUI;
    [SerializeField] Transform scoreLocation;
    PlayerConfigBridge playerConfigBridge;
    GameObject scoreUI;

    void Start()
    {
        playerConfigBridge = GetComponentInParent<PlayerConfigBridge>();
        InitDamageScore();
    }
    private void OnEnable()
    {
        if (scoreUI != null)
        {
            scoreUI.SetActive(true);
        }
    }

    private void InitDamageScore()
    {
        playerConfigBridge.PlayerConfig.DamageDealtToChicken = 0;
        scoreUI = Instantiate(chickenDamageScoreUI);
        scoreUI.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
        scoreUI.name = playerConfigBridge.PlayerConfig.name + "_Score";
        int score = (int)playerConfigBridge.PlayerConfig.DamageDealtToChicken;
        scoreUI.GetComponentInChildren<TextMeshProUGUI>().SetText(score.ToString());
    }

    void Update()
    {
        if (scoreUI != null)
        {
            SetScorePos();
            UpdatePlayerDamageScore();
        }
    }

    private void SetScorePos()
    {
        scoreUI.transform.position = scoreLocation.position;
    }

    void UpdatePlayerDamageScore()
    {
        int score = (int)playerConfigBridge.PlayerConfig.DamageDealtToChicken;
        scoreUI.GetComponentInChildren<TextMeshProUGUI>().SetText(score.ToString());
    }

    private void OnDisable()
    {
        if (scoreUI != null)
        {
            scoreUI.SetActive(false);
        }
    }
}
