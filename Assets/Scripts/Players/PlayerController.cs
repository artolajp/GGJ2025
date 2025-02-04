using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Properties.
    [SerializeField] public int playerNumber = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject particleBubblePop;

    // Movement.
    private Rigidbody rigidBody;
    private float movement_x;
    private float movement_y;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnMovement(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movement_x = movementVector.x;
        movement_y = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movement_x, 0.0f, movement_y);

        rigidBody.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HurtBox")
        {
            FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_BubblePop_1", "Sound_BubblePop_2", "Sound_BubblePop_3", "Sound_BubblePop_4", "Sound_BubblePop_5", "Sound_BubblePop_6");

            Instantiate(particleBubblePop, transform.position, Quaternion.identity);

            Actions.PlayerDeath?.Invoke(this);
            Destroy(gameObject);
        }

        if (other.tag == "WinBox")
        {
            FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_BubblePop_1", "Sound_BubblePop_2", "Sound_BubblePop_3", "Sound_BubblePop_4", "Sound_BubblePop_5", "Sound_BubblePop_6");
            FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(1, 1, "Sound_Win");
            
            Instantiate(particleBubblePop, transform.position, Quaternion.identity);

            Actions.PlayerScored?.Invoke(this);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "WindBox")
        {
            WindBox fieldBox = other.GetComponent<WindBox>();

            if (fieldBox != null)
            {
                float windStrength = fieldBox.WindForce;
                Vector3 collisionNormal = other.transform.forward;

                rigidBody.AddForce(collisionNormal * windStrength);
            }
        }
    }
}