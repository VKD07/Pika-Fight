using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame ()
    {
        
        SceneManager.LoadScene("ShrinkLevel1"); // or to use scene numbers remoive brackets and type the number (also drag scene into build settings)

    }

    public void QuitGame ()
    {
        Debug.Log("quit test");
        Application.Quit();
    }
}
