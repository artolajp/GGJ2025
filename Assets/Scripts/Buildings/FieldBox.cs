using UnityEngine;

public class FieldBox : MonoBehaviour
{
    [Header("Wind Settings")]
    [SerializeField]
    private float windForce = 5f;

    public float WindForce
    {
        get { return windForce; }
        set { windForce = value; }
    }

    private void Awake()
    {
        FindObjectOfType<AudioController>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_Fan");
    }
}
