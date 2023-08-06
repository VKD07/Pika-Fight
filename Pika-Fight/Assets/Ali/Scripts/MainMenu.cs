using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int NumberSceneToLoad;

    IEnumerator PlayGame1() 
    {
        yield return new WaitForSeconds(1.0f);
    
        SceneManager.LoadScene(NumberSceneToLoad);
    }

    //IEnumerator QuitGame1() 
    //{
      //  yield return new WaitForSeconds(1.0f);
    
      //  Application.Quit();
    //}
    

    public void PlayGame ()
    {
        StartCoroutine(PlayGame1());
     
       //SceneManager.LoadScene(NumberSceneToLoad);
       // SceneManager.LoadScene("ShrinkLevel1"); // or to use scene numbers remoive brackets and type the number (also drag scene into build settings)
    }


    public void QuitGame ()
    {
        Debug.Log("quit test");
        Application.Quit();
       // StartCoroutine(Application.QuitGame1());
    }
}
