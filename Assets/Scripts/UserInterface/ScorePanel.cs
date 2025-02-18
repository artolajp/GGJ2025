using UnityEngine;
using UnityEngine.UI;
using System;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private Slider sliderPlayer_01;
    [SerializeField] private Slider sliderPlayer_02;

    private void Awake()
    {
        gameObject.SetActive(true);
        sliderPlayer_01.value = GameData.Score_01;
        sliderPlayer_02.value = GameData.Score_02;
        sliderPlayer_01.maxValue = GameData.TargetScore;
        sliderPlayer_02.maxValue = GameData.TargetScore;
    }

    public void Show(int scorePlayer_01, int scorePlayer_02, int maximumScore)
    {
        gameObject.SetActive(true);
        sliderPlayer_01.value = scorePlayer_01;
        sliderPlayer_02.value = scorePlayer_02;
        sliderPlayer_01.maxValue = maximumScore;
        sliderPlayer_02.maxValue = maximumScore;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}