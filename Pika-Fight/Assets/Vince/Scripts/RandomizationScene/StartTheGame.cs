using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartTheGame : MonoBehaviour
{
    [SerializeField] float delayTime = 3f;
    [SerializeField] UnityEvent startGamePlay;
    bool doneChoosing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doneChoosing)
        {
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
