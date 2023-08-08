using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int MenuNumberSceneToLoad;
    public int OptionNumberSceneToLoad;
    public int BackButtonNumberSceneToLoad;

    IEnumerator PlayGame1() 
    {
        yield return new WaitForSeconds(1.0f);
    
        SceneManager.LoadScene(MenuNumberSceneToLoad);
    }

    public void PlayGame ()
    {
        StartCoroutine(PlayGame1());
     
       //SceneManager.LoadScene(NumberSceneToLoad);
       // SceneManager.LoadScene("ShrinkLevel1"); // or to use scene numbers remoive brackets and type the number (also drag scene into build settings)
    }

    IEnumerator OptionsButton1()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(OptionNumberSceneToLoad);
    }

    public void OptionsButton ()
    {
        StartCoroutine(OptionsButton1());
    }

    IEnumerator BackButton1()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(BackButtonNumberSceneToLoad);
    }

    public void BackButton()
    {
        StartCoroutine(BackButton1());
    }


    public void QuitGame ()
    {
        Debug.Log("quit test");
        Application.Quit();
       // StartCoroutine(Application.QuitGame1());
    }
}
