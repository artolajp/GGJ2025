using System.Collections;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private Slider sliderP1;
    [SerializeField] private Slider sliderP2;

    private Action onHide;

    public void Show(int scoreP1, int scoreP2, int maxScore, Action onHide)
    {
        gameObject.SetActive(true);
        sliderP1.value = scoreP1;
        sliderP2.value = scoreP2;
        sliderP1.maxValue = maxScore;
        sliderP2.maxValue = maxScore;
        this.onHide = onHide;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        onHide?.Invoke();
    }
    
    
}
