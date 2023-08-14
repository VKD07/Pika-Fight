using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIndicator : MonoBehaviour
{
    [SerializeField] GameObject uiIndicatorPrefab;
    [SerializeField] Transform uiLocation;
    GameObject ui;
    void Start()
    {
        InitUIInidcator();
    }

    private void OnEnable()
    {
        if (ui != null)
        {
            ui.SetActive(true);
        }
    }

    private void InitUIInidcator()
    {
        ui = Instantiate(uiIndicatorPrefab);
        ui.transform.SetParent(GameObject.Find("PlayerHealthManager").transform);
    }

    void Update()
    {
        if (ui != null && uiLocation != null)
        {
            SetIndicatorPos();
        }
    }

    public void SetActiveUIIndicator(bool value)
    {
        if(ui != null)
        {
            ui.SetActive(value);
        }
    }

    private void SetIndicatorPos()
    {
        ui.transform.position = uiLocation.position;
    }

    public void TriggerAnimation()
    {
        LeanTween.scale(ui, new Vector3(0.2153192f, 0.2153192f, 0.2153192f), 0.2f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(ui, new Vector3(0.1962443f, 0.1962443f, 0.1962443f), 0.2f).setDelay(0.2f).setEase(LeanTweenType.easeInBack);
    }

    public GameObject UIIndicatorPrefab { set => uiIndicatorPrefab = value; }
    public Transform UILocation { set => uiLocation = value;}
}
