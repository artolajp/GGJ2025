using UnityEngine;
using UnityEngine.UI;
using System;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private Slider sliderPlayer_01;
    [SerializeField] private Slider sliderPlayer_02;

    private Action onHide;

    public void Show(int scorePlayer_01, int scorePlayer_02, int maximumScore, Action setOnHide)
    {
        gameObject.SetActive(true);
        sliderPlayer_01.value = scorePlayer_01;
        sliderPlayer_02.value = scorePlayer_02;
        sliderPlayer_01.maxValue = maximumScore;
        sliderPlayer_02.maxValue = maximumScore;
        onHide = setOnHide;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        onHide?.Invoke();
    }
}