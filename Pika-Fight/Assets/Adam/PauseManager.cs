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
    [SerializeField] int index;
    bool startDelay = false;
    public float y;

    void Update()
    {
        foreach (var key in playerControls)
        {
            PauseGame(key.PauseKey);

            if (pausePannel.activeSelf)
            {
                MovePointers(key);
            }
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
        }
    }

    private void MovePointers(PlayerControls playerControls)
    {
      y = Input.GetAxis($"{playerControls.GetMovementAxes}_Vertical");

        if (y < 0 && index < buttons.Length - 1)
        {
            OnMovePointer.Invoke();
            index++;
        }
        else if (y > 0 && index > 0)
        {
            OnMovePointer.Invoke();
            index--;
        }
        SelectBtn(playerControls, index);
        buttons[index].Select(); // Ensure the selected button is explicitly set
    }
    void SelectBtn(PlayerControls playerControls, int index)
    {
        if (Input.GetKeyDown(playerControls.SelectKey))
        {
            OnSelectBtn.Invoke();
            buttons[index].onClick.Invoke();
        }
    }
    private IEnumerator TimerDelay()
    {
        startDelay = true;
        yield return new WaitForSeconds(delayTime);
        startDelay = false;
    }

    public void Resume()
    {
        if (pausePannel.activeSelf)
        {
            pausePannel.SetActive(false);
            Time.timeScale = 1;
            OnResume.Invoke();
        }
    }

    public void Exit()
    {
        OnResume.Invoke();
        SceneManager.LoadScene("StartMenu");
    }
}
