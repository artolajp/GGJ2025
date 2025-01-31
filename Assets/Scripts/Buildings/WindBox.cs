using UnityEngine;

public class WindBox : MonoBehaviour
{
    [SerializeField] private float windForce = 5f;

    public float WindForce
    {
        get { return windForce; }
        set { windForce = value; }
    }

    private void Awake()
    {
        FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_Fan");
    }
}
