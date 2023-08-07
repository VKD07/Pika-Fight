using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenDebuf : MonoBehaviour
{
    [SerializeField] GameObject chickenModel;
    [SerializeField] PlayerConfigBridge playerConfigBridge;
    [SerializeField] UnityEvent OnChickenMode;
    [SerializeField] UnityEvent NormalMode;
    float chickenDuration;
    private void OnEnable()
    {
        NormalForm(false);
        StartCoroutine(DisableChickenForm());
    }

    void NormalForm(bool enable)
    {
        transform.parent.Find($"{playerConfigBridge.PlayerConfig.CharacterName}_Head").gameObject.SetActive(enable);
        transform.parent.Find($"{playerConfigBridge.PlayerConfig.CharacterName}_Body").gameObject.SetActive(enable);

        if(enable == false)
        {
            chickenModel.SetActive(true);
        }
        else
        {
            chickenModel.SetActive(false);
        }
    }

    IEnumerator DisableChickenForm()
    {
        yield return new WaitForSeconds(5);
        NormalForm(true);
        gameObject.SetActive(false);
    }

    public float ChickenDuration {  set => chickenDuration = value; }
}
