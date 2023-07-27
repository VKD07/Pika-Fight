using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerConfigBridge))]
public class GemScoreUI : MonoBehaviour
{
    [SerializeField] GameObject gemScoreUI;
    [SerializeField] Transform scoreLocation;
    PlayerConfigBridge playerConfigBridge;
    GameObject scoreUI;
    void Start()
    {
        playerConfigBridge = GetComponent<PlayerConfigBridge>();
        InitGemScore();
    }
    private void OnEnable()
    {
        if (scoreUI != null)
        {
            scoreUI.SetActive(true);
        }
    }

    private void InitGemScore()
    {
        scoreUI = Instantiate(gemScoreUI);
        scoreUI.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
        scoreUI.name = playerConfigBridge.PlayerConfig.name + "_Score";
        scoreUI.GetComponentInChildren<TextMeshProUGUI>().SetText(playerConfigBridge.PlayerConfig.GemScore.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreUI != null)
        {
            SetScorePos();
            UpdatePlayerGemScore();
        }
    }

    private void SetScorePos()
    {
        scoreUI.transform.position = scoreLocation.position;
    }

    void UpdatePlayerGemScore()
    {
        scoreUI.GetComponentInChildren<TextMeshProUGUI>().SetText(playerConfigBridge.PlayerConfig.GemScore.ToString());
    }

    public void AddScore(float value)
    {
        playerConfigBridge.PlayerConfig.GemScore += value;
    }

    private void OnDisable()
    {
        if (scoreUI != null)
        {
            scoreUI.SetActive(false);
        }
    }
}
