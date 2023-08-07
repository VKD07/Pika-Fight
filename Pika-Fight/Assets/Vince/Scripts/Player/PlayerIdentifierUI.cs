using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIdentifierUI : MonoBehaviour
{
    [SerializeField] GameObject playerColorIdentifierUI;
    [SerializeField] Transform uiLocation;
    Color playercolor;
    GameObject uiIdentifier;
    //Dash UI
   [SerializeField] FloatReference dashCoolDown;
    float elapsedTime = 0f;
    bool dashed;
    bool filledUp = true;
    Image dashUI;
    void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        uiIdentifier = Instantiate(playerColorIdentifierUI);
        uiIdentifier.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
        dashUI = uiIdentifier.transform.Find("DashUI").GetComponent<Image>();
        SetColor(playercolor);

    }

    void Update()
    {
        if (uiIdentifier != null)
        {
            SetUIPos();
            UpdateDashUI();
        }
    }

    private void SetUIPos()
    {
        uiIdentifier.transform.position = uiLocation.position;
    }

    void SetColor(Color color)
    {
        Image [] uiImage = uiIdentifier.GetComponentsInChildren<Image>();
        for (int i = 0; i < uiImage.Length; i++)
        {
            uiImage[i].color = color;
        }
    }

    void UpdateDashUI()
    {
        if (dashed && dashUI != null)
        {
            if (filledUp)
            {
                filledUp = false;
                dashUI.fillAmount = 0;
                elapsedTime = 0f;
            }
            
            elapsedTime += Time.deltaTime;
            
            float fillAmount = elapsedTime / dashCoolDown.Value;
            fillAmount = Mathf.Clamp01(fillAmount);
            
            dashUI.fillAmount = fillAmount;
            if (fillAmount >= 1f)
            {
                dashed = false;
                filledUp = true;
            }
        }
    }

    public bool SetDash { set { dashed = value; } }

    public Color PlayerColor { set => playercolor = value; }
}
