using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePannel;
    [SerializeField] PlayerControls[] playerControls;
    [SerializeField] Button[] buttons;
    [SerializeField] UnityEvent OnPause;
    [SerializeField] UnityEvent OnResume;
    [SerializeField] UnityEvent OnMovePointer;
    [SerializeField] UnityEvent OnSelectBtn;
    [SerializeField] float delayTime = 0.2f;
    int index;
    bool startDelay = false;
    public float y;

    void Update()
    {
        foreach (var key in playerControls)
        {
            PauseGame(key.PlayerStartKey);
        }
    }

    void PauseGame(KeyCode pausekey)
    {
        if (Input.GetKeyDown(pausekey))
        {
            if (!pausePannel.activeSelf)
            {
                pausePannel.SetActive(true);
                Time.timeScale = 0;
                OnPause.Invoke();
            }
            else
            {
                OnResume.Invoke();
                Time.timeScale = 1;
                pausePannel.SetActive(false);
            }
        }
    }

    private void MovePointers(PlayerControls playerControls)
    {
      y = Input.GetAxis($"{playerControls.GetMovementAxes}_Vertical");

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

    public void InvokeOnResume()
    {
        OnResume.Invoke();
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

    public void Resume()
    {
        if (pausePannel.activeSelf)
        {
            pausePannel.SetActive(false);
            Time.timeScale = 1;
            OnPause.Invoke();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
