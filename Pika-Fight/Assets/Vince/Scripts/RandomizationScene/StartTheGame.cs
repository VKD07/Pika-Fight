using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartTheGame : MonoBehaviour
{
    [SerializeField] float delayTime = 3f;
    [SerializeField] KeyCode startKey = KeyCode.Joystick1Button7;
    [SerializeField] UnityEvent OnButtonpressed;
    [SerializeField] UnityEvent startGamePlay;
    bool doneChoosing;

    // Update is called once per frame
    void Update()
    {
        if (doneChoosing && Input.GetKeyDown(startKey) || doneChoosing && Input.GetKeyDown(KeyCode.Return))
        {
            OnButtonpressed.Invoke();
            StartCoroutine(LoadSceneWithDelay());
            doneChoosing = false;
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        startGamePlay.Invoke();
    }

    public bool DoneChoosing { set=> doneChoosing = value; }
}
