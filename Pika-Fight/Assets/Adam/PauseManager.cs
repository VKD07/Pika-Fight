using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePannel;


    void Update()
    {
        if (!pausePannel.activeSelf)
        {
            pausePannel.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {
            pausePannel.SetActive(false);
        }
    }
}
