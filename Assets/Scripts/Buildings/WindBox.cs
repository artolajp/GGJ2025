using UnityEngine;

public class WindBox : MonoBehaviour
{
    [SerializeField] private float windForce = 5f;
    AudioSource myWindSound;

    public float WindForce
    {
        get { return windForce; }
        set { windForce = value; }
    }

    private void Start()
    {
        myWindSound = FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(0.5f, 1.5f, "Sound_Fan");
    }

    private void OnDestroy()
    {
        if (myWindSound != null)
        {
            FindAnyObjectByType<AudioManager>().StopAudioSource(myWindSound);

            myWindSound = null;
        }
    }
}
