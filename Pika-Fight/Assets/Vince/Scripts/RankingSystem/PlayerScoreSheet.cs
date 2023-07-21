using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreSheet : MonoBehaviour
{
    [SerializeField] Image[] scoreImg;
    [SerializeField] FloatReference currentScore;

    private void Start()
    {
    }
    private void Update()
    {
        for (int i = 0; i < currentScore.Value; i++)
        {
            scoreImg[i].color = Color.white;
        }
    }
    public float SetScoreValue { set => currentScore.Value = value; }
}
