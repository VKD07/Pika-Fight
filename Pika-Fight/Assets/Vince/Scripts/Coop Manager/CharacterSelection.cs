using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] CharacterObjectPooling characterPool;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] RectTransform[] pointers;
    [SerializeField] RectTransform[] characters;
    [SerializeField] int[] characterIndeces;

    [Header("Pointer Settings")]
    [SerializeField] float pointerDelay = 0.2f;
    [SerializeField] bool[] startDelays;
    [Space]
    [SerializeField] UnityEvent OnPointerMove;
    [SerializeField] UnityEvent OnCharacterChoose;

    void Update()
    {
        EnablePointers();
    }

    private void EnablePointers()
    {
        #region Prev Code
        //if (playerJoinedData.GetPlayersJoined[0] != null)
        //{
        //    pointers[0].gameObject.SetActive(true);
        //    MovePointers(playerJoinedData.GetPlayersJoined[0].Player_Controls, 0);
        //    pointers[0].position = characters[characterIndeces[0]].position;
        //}

        //if (playerJoinedData.GetPlayersJoined[1] != null)
        //{
        //    pointers[1].gameObject.SetActive(true);
        //    MovePointers(playerJoinedData.GetPlayersJoined[1].Player_Controls, 1);
        //    pointers[1].position = characters[characterIndeces[1]].position;
        //}

        //if (playerJoinedData.GetPlayersJoined[2] != null)
        //{
        //    pointers[2].gameObject.SetActive(true);
        //    MovePointers(playerJoinedData.GetPlayersJoined[2].Player_Controls, 2);
        //    pointers[2].position = characters[characterIndeces[2]].position;
        //}

        //if (playerJoinedData.GetPlayersJoined[3] != null)
        //{
        //    pointers[3].gameObject.SetActive(true);
        //    MovePointers(playerJoinedData.GetPlayersJoined[3].Player_Controls, 3);
        //    pointers[3].position = characters[characterIndeces[3]].position;
        //}
        #endregion

        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null)
            {
                if (!playerJoinedData.GetPlayersJoined[i].PlayerIsReady)
                {
                    pointers[i].gameObject.SetActive(true);
                    MovePointers(playerJoinedData.GetPlayersJoined[i].Player_Controls, i);
                    pointers[i].position = characters[characterIndeces[i]].position;
                    UpdateCharacterOnPointerMove(i);
                }
                else
                {
                    RechooseCharacter(i);
                }
            }
        }
    }

    private void MovePointers(PlayerControls playerControl, int i)
    {
        float x = Input.GetAxis($"{playerControl.GetMovementAxes}_Horizontal");
        float y = Input.GetAxis($"{playerControl.GetMovementAxes}_Vertical");

        if (!startDelays[i])
        {
            if (x > 0 && characterIndeces[i] < characters.Length - 1)
            {
                OnPointerMove.Invoke();
                StartCoroutine(TimerDelay(i));
                characterIndeces[i]++;
            }
            else if (x < 0 && characterIndeces[i] > 0)
            {
                OnPointerMove.Invoke();
                StartCoroutine(TimerDelay(i));
                characterIndeces[i]--;
            }
            else if (y > 0 && characterIndeces[i] >= 5)
            {
                OnPointerMove.Invoke();
                StartCoroutine(TimerDelay(i));
                characterIndeces[i] -= 5;
            }
            else if (y < 0 && characterIndeces[i] < characters.Length - 5)
            {
                OnPointerMove.Invoke();
                StartCoroutine(TimerDelay(i));
                characterIndeces[i] += 5;
            }
        }
    }

    IEnumerator TimerDelay(int i)
    {
        startDelays[i] = true;
        yield return new WaitForSeconds(pointerDelay);
        startDelays[i] = false;
    }


    void UpdateCharacterOnPointerMove(int characterBtnIndex)
    {
        characterPool.GetListOfCharacterSlots[characterBtnIndex].PickItem(characterIndeces[characterBtnIndex]);
        StartCoroutine(ChooseCharacter(characterBtnIndex));
    }

    IEnumerator ChooseCharacter(int btnIndex)
    {
        yield return new WaitForSeconds(0.5f);
        if (Input.GetKeyDown(playerJoinedData.GetPlayersJoined[btnIndex].Player_Controls.PlayerReadyKey) && !characters[characterIndeces[btnIndex]].GetComponent<CharacterBtn>().CharacterIsTaken)
        {
            OnCharacterChoose.Invoke();
            // playerJoinedData.GetPlayersJoined[btnIndex].PlayerCharacter = characters[characterIndeces[btnIndex]].GetComponent<CharacterBtn>().GetCharaterPrefab;
            playerJoinedData.GetPlayersJoined[btnIndex].CharacterName = characters[characterIndeces[btnIndex]].GetComponent<CharacterBtn>().CharacterName;
            playerJoinedData.GetPlayersJoined[btnIndex].CharacterSprite = characters[characterIndeces[btnIndex]].GetComponent<CharacterBtn>().CharacterSprite;
            characters[characterIndeces[btnIndex]].GetComponent<CharacterBtn>().CharacterIsTaken = true;
            playerJoinedData.GetPlayersJoined[btnIndex].PlayerIsReady = true;
            playerJoinedData.GetPlayersJoined[btnIndex].PlayerCharacter = characters[characterIndeces[btnIndex]].GetComponent<CharacterBtn>().GetCharaterPrefab;
            //Animation when chosen
            characterPool.GetListOfCharacterSlots[btnIndex].CharacterList[characterIndeces[btnIndex]].GetComponent<Animator>().SetTrigger("Chosen");
        }
    }

    void RechooseCharacter(int btnIndex)
    {
        if (playerJoinedData.GetPlayersJoined[btnIndex].PlayerIsReady && Input.GetKeyDown(playerJoinedData.GetPlayersJoined[btnIndex].Player_Controls.PlayerUnreadyKey))
        {
            characters[characterIndeces[btnIndex]].GetComponent<CharacterBtn>().CharacterIsTaken = false;
            playerJoinedData.GetPlayersJoined[btnIndex].PlayerIsReady = false;
        }
    }
}
