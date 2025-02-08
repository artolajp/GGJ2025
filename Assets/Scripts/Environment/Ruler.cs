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
            Instantiate(particleRuler, transform.position, Quaternion.identity);
            transform.position = new Vector3(transform.position.x, transform.position.y, -18f);
            speed = 0f;
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
