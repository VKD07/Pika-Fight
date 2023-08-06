using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VersusManager : MonoBehaviour
{
    [NonReorderable]
    [SerializeField] VersusSlotManager[] versusSlotManager;
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject versusUI;
    [SerializeField] GameObject readyTxt;
    [SerializeField] GameObject fightTxt;

    private void Start()
    {
        StartCoroutine(FindPlayers());
    }

    IEnumerator FindPlayers()
    {
        yield return new WaitForSeconds(0.5f);
        players = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(ReadyTextEnable());
        DisablePlayerMovement();
        SetUpRenderTexture();
    }

    void SetUpRenderTexture()
    {
        if (players.Length == 2)
        {
            versusSlotManager[0].slotsParent.SetActive(true);
            LeanTween.moveLocal(versusSlotManager[0].imageUI[1], new Vector3(-462, 0, 0), .7f).setEaseInElastic();
            LeanTween.moveLocal(versusSlotManager[0].imageUI[0], new Vector3(458, 0, 0), .7f).setEaseInElastic();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.Find("PlayerCamera").gameObject.SetActive(true);
                players[i].transform.Find("PlayerCamera").GetComponent<Camera>().targetTexture = versusSlotManager[0].renderTextures[i];
            }
            LeanTween.moveLocal(versusSlotManager[0].imageUI[1], new Vector3(-462, 1153, 0), .1f).setDelay(3);
            LeanTween.moveLocal(versusSlotManager[0].imageUI[0], new Vector3(458, -1118, 0), .1f).setDelay(3);
            StartCoroutine(DisableUI());

        }
        else if (players.Length == 3) // 3 Players
        {
            versusSlotManager[1].slotsParent.SetActive(true);
            LeanTween.moveLocal(versusSlotManager[1].imageUI[0], new Vector3(609, -1, 0), .7f).setEaseInElastic();
            LeanTween.moveLocal(versusSlotManager[1].imageUI[1], new Vector3(2, -1, 0), .7f).setEaseInElastic();
            LeanTween.moveLocal(versusSlotManager[1].imageUI[2], new Vector3(-607, -1, 0), .7f).setEaseInElastic();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.Find("PlayerCamera").gameObject.SetActive(true);
                players[i].transform.Find("PlayerCamera").GetComponent<Camera>().targetTexture = versusSlotManager[1].renderTextures[i];
            }
            LeanTween.moveLocal(versusSlotManager[1].imageUI[0], new Vector3(609, -1153, 0), .1f).setDelay(3);
            LeanTween.moveLocal(versusSlotManager[1].imageUI[1], new Vector3(2, 1118, 0), .1f).setDelay(3);
            LeanTween.moveLocal(versusSlotManager[1].imageUI[2], new Vector3(-607, -1236, 0), .1f).setDelay(3);
            StartCoroutine(DisableUI());
        }

        else if (players.Length == 4) // 4 Players
        {
            versusSlotManager[2].slotsParent.SetActive(true);
            LeanTween.moveLocal(versusSlotManager[2].imageUI[0], new Vector3(684, 0, 0), .7f).setEaseInElastic();
            LeanTween.moveLocal(versusSlotManager[2].imageUI[1], new Vector3(227, 0, 0), .7f).setEaseInElastic();
            LeanTween.moveLocal(versusSlotManager[2].imageUI[2], new Vector3(-229, 0, 0), .7f).setEaseInElastic();
            LeanTween.moveLocal(versusSlotManager[2].imageUI[3], new Vector3(-683, 1, 0), .7f).setEaseInElastic();

            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.Find("PlayerCamera").gameObject.SetActive(true);
                players[i].transform.Find("PlayerCamera").GetComponent<Camera>().targetTexture = versusSlotManager[2].renderTextures[i];
            }
            LeanTween.moveLocal(versusSlotManager[2].imageUI[0], new Vector3(684, -1095, 0), .1f).setDelay(3);
            LeanTween.moveLocal(versusSlotManager[2].imageUI[1], new Vector3(227, 1069, 0), .1f).setDelay(3);
            LeanTween.moveLocal(versusSlotManager[2].imageUI[2], new Vector3(-229, -1084, 0), .1f).setDelay(3);
            LeanTween.moveLocal(versusSlotManager[2].imageUI[3], new Vector3(-683, 1061, 0), .1f).setDelay(3);

            StartCoroutine(DisableUI());
        }
    }

    IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(3.1f);
        versusUI.SetActive(false);
        readyTxt.SetActive(false);
        //EnablePlayerMovement(true, 1f);
        EnablePlayerMovement(true);
    }

    void DisablePlayerMovement()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMovement>().enabled = false;
        }
    }

    void EnablePlayerMovement(bool value)
    {
        fightTxt.SetActive(true);
        LeanTween.scale(fightTxt, new Vector3(0.6f, 0.6f, 0.6f), .8f).setEaseInOutBack();
        LeanTween.scale(fightTxt, new Vector3(0.4f, 0.4f, 0.4f), .8f).setEaseOutBack().setDelay(0.4f);
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMovement>().enabled = value;
        }
        StartCoroutine(FightTxtDisable());
    }

    IEnumerator ReadyTextEnable()
    {
        yield return new WaitForSeconds(0.6f);
        readyTxt.SetActive(true);
        LeanTween.scale(readyTxt, new Vector3(0.6f, 0.6f, 0.6f), .8f).setEaseInOutBack();
        LeanTween.scale(readyTxt, new Vector3(0.4f, 0.4f, 0.4f), .8f).setEaseOutBack().setDelay(0.4f);
    }

    IEnumerator FightTxtDisable()
    {
        yield return new WaitForSeconds(1.3f);
        fightTxt.SetActive(false);
    }
}
