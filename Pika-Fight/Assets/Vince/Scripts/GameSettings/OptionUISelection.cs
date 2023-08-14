using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class OptionUISelection : MonoBehaviour
{
    [SerializeField] GameObject[] selections;
    public int index;
    bool moveDelay;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("PlayerJoin");
        }
        //MoveSelection();
    }

    private void MoveSelection()
    {
        float x = Input.GetAxis("WASD_Horizontal");
        float y = Input.GetAxis("WASD_Vertical");


        if (!moveDelay)
        {
            if (x > 0 && index < selections.Length - 1)
            {
                StartCoroutine(TimerDelay());
                index++;
            }
            else if (x < 0 && index > 0)
            {
                StartCoroutine(TimerDelay());
                index--;
            }
            else if (y > 0 && index >= 3)
            {
                StartCoroutine(TimerDelay());
                index -= 3;
            }
            else if (y < 0 && index < selections.Length - 3)
            {
                StartCoroutine(TimerDelay());
                index += 3;
            }
        }
    }

    IEnumerator TimerDelay()
    {
        moveDelay = true;
        yield return new WaitForSeconds(0.2f);
        moveDelay = false;
    }
}
