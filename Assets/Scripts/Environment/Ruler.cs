using UnityEngine;

public class Ruler : MonoBehaviour
{
    private float speed = 0f;
    [SerializeField] GameObject particleRuler;

    private void OnEnable()
    {
        Actions.spelunkyTime += ChangeSpeed;
    }

    private void OnDisable()
    {
        Actions.spelunkyTime -= ChangeSpeed;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);

        if (transform.position.z > 10f)
        {
            FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.2f, 1.2f, "Sound_BuildPlaced_1", "Sound_BuildPlaced_2", "Sound_BuildPlaced_3");

            Instantiate(particleRuler, transform.position, Quaternion.identity);

            transform.position = new Vector3(transform.position.x, transform.position.y, -18f);
            speed = 0f;

            Instantiate(particleRuler, transform.position, Quaternion.identity);

            Actions.rulerIsDone?.Invoke();
        }
    }

    private void ChangeSpeed(bool isSpelunkyTime)
    {
        if (isSpelunkyTime == true)
        {
            speed = 4f;
        }
        else
        {
            speed = 16f;
        }
    }
}
