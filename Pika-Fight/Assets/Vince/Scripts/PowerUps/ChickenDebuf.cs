using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenDebuf : MonoBehaviour
{
    [SerializeField] GameObject chickenModel;
    [SerializeField] PlayerConfigBridge playerConfigBridge;
    [SerializeField] float chickenFormDuration = 5f;
    [SerializeField] UnityEvent OnChickenMode;
    [SerializeField] UnityEvent NormalMode;
    float chickenDuration;
    private void OnEnable()
    {
        OnChickenMode.Invoke();
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
        yield return new WaitForSeconds(chickenFormDuration);
        NormalMode.Invoke();
        NormalForm(true);
        this.enabled = false;
    }

    public float ChickenDuration {  set => chickenDuration = value; }
}
