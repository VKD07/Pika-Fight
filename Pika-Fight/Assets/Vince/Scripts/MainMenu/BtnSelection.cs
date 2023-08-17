using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static PlayerControls;

public class BtnSelection : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] PlayerControls[] playerControls;
    [SerializeField] float delayTime = 0.2f;
    [SerializeField] UnityEvent OnMovePointer;
    [SerializeField] UnityEvent OnSelectBtn;
    int index = 0;
    bool startDelay = false;

    private void Start()
    {
        buttons[index].Select();
    }
    private void Update()
    {
        foreach (var controls in playerControls)
        {
            MovePointers(controls);
        }
    }

    private void MovePointers(PlayerControls playerControls)
    {
        float y = Input.GetAxis($"{playerControls.GetMovementAxes}_Vertical");

        if (!startDelay)
        {
            if (y < 0 && index < buttons.Length - 1)
            {
                StartCoroutine(TimerDelay());
                index++;
                buttons[index].Select();

            }
            else if (y > 0 && index > 0)
            {
                StartCoroutine(TimerDelay());
                index--;
                buttons[index].Select();
            }
            SelectBtn(playerControls, index);
        }
    }

    void SelectBtn(PlayerControls playerControls, int index)
    {
        if (Input.GetKeyDown(playerControls.GetAttackKey))
        {
            OnSelectBtn.Invoke();
            buttons[index].onClick.Invoke();
        }
    }
    private IEnumerator TimerDelay()
    {
        startDelay = true;
        OnMovePointer.Invoke();
        yield return new WaitForSeconds(delayTime);
        startDelay = false;
    }
}
