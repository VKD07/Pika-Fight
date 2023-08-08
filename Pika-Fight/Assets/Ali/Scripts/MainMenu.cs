using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int MenuNumberSceneToLoad;
    public int OptionNumberSceneToLoad;
    public int BackToMenuButtonNumberSceneToLoad;
    public int ControlSheetButtonToLoad;
    public int BackToOptionsNumberSceneToLoad;

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



    IEnumerator BackToMenuButton1()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(BackToMenuButtonNumberSceneToLoad);
    }

    public void BackToMenuButton()
    {
        StartCoroutine(BackToMenuButton1());
    }



    IEnumerator BackToOptionsButton1()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(BackToOptionsNumberSceneToLoad);
    }

    public void BackToOptionsButton()
    {
        StartCoroutine(BackToOptionsButton1());
    }



    IEnumerator OpenButton1()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(ControlSheetButtonToLoad);
    }

    public void OpenButton()
    {
        StartCoroutine(OpenButton1());
    }




    public void QuitGame ()
    {
        Debug.Log("quit test");
        Application.Quit();
       // StartCoroutine(Application.QuitGame1());
    }
}
