using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Properties.
    [Header("Player properties.")]
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
            FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_BubblePop_01", "Sound_BubblePop_02", "Sound_BubblePop_03");

            Instantiate(particleBubblePop, transform.position, Quaternion.identity);

            Actions.PlayerDeath?.Invoke(this);
            Destroy(gameObject);
        }

        if (other.tag == "WinBox")
        {
            FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_BubblePop_01", "Sound_BubblePop_02", "Sound_BubblePop_03");

            FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(1, 1, "Sound_Win");
            
            Instantiate(particleBubblePop, transform.position, Quaternion.identity);

            Actions.PlayerScored?.Invoke(this);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "FieldBox")
        {
            FieldBox fieldBox = other.GetComponent<FieldBox>();

            if (fieldBox != null)
            {
                float windStrength = fieldBox.WindForce;
                Vector3 collisionNormal = other.transform.forward;

                rigidBody.AddForce(collisionNormal * windStrength);
            }
        }
    }
}