using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerScoreSheet : MonoBehaviour
{
    [SerializeField] Image[] scoreImg;
    [SerializeField] FloatReference currentScore;
    float initScore;
    [Header("Animation")]
    [SerializeField] Vector3 scorePanelInitScale;
    [SerializeField] Vector3 toScale;
    bool animated;
    [SerializeField] UnityEvent OnAddingScore;
    private void Start()
    {
        initScore = currentScore.Value;
    }
    private void Update()
    {
        for (int i = 0; i < currentScore.Value; i++)
        {
            scoreImg[i].color = Color.white;

            if (scoreImg[i].color == Color.white && !animated && currentScore.Value > initScore)
            {
                OnAddingScore.Invoke();
                animated = true;
                UIAnimation(gameObject);
            }
        }
    }

    public void UIAnimation(GameObject ui)
    {
        LeanTween.scale(ui, toScale, .3f).setEaseInOutElastic();
        LeanTween.scale(ui, scorePanelInitScale, .3f).setEaseInSine().setDelay(0.2f);
    }
    public float SetScoreValue { set => currentScore.Value = value; }
}
