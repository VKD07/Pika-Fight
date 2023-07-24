using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelActivator : MonoBehaviour
{
    [SerializeField] GameObject[] listOf3DModels; 
   
    public void DisableAll3DModels()
    {
        for (int i = 0; i < listOf3DModels.Length; i++)
        {
            listOf3DModels[i].SetActive(false);
        }
    }
}
