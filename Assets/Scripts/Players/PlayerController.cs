using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Properties.
    [SerializeField] private int playerNumber = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject particleBubblePop;
    [SerializeField] private GameObject particleBubbleTeleported;

    // Movement.
    private Rigidbody rigidBody;
    private float movement_x;
    private float movement_y;
    private float slowdownForce = 80f;

    public int PlayerNumber
    {
        get { return playerNumber; }
        set { playerNumber = value; }
    }

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

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "HurtBox":
            {
                FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_BubblePop_1", "Sound_BubblePop_2", "Sound_BubblePop_3", "Sound_BubblePop_4", "Sound_BubblePop_5", "Sound_BubblePop_6");

                Instantiate(particleBubblePop, transform.position, Quaternion.identity);

                Actions.PlayerDeath?.Invoke(this);
                Destroy(gameObject);
            }
            break;

            case "WinBox":
            {
                FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_BubblePop_1", "Sound_BubblePop_2", "Sound_BubblePop_3", "Sound_BubblePop_4", "Sound_BubblePop_5", "Sound_BubblePop_6");
                FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(1, 1, "Sound_Win");

                Instantiate(particleBubblePop, transform.position, Quaternion.identity);

                Actions.PlayerScored?.Invoke(this);
                Destroy(gameObject);
            }
            break;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        switch (collider.tag)
        {
            case "HoneyBox":
            {
                float getForce = slowdownForce;
                Vector3 oppositeForce = -rigidBody.linearVelocity.normalized;

                if (rigidBody.linearVelocity.magnitude < 4f)
                {
                    getForce = 8f;
                }

                rigidBody.AddForce(oppositeForce * getForce);
            }
            break;

            case "WindBox":
            {
                WindBox fieldBox = collider.GetComponent<WindBox>();

                if (fieldBox != null)
                {
                    float windStrength = fieldBox.WindForce;
                    Vector3 collisionNormal = collider.transform.forward;

                    rigidBody.AddForce(collisionNormal * windStrength);
                }
            }
            break;
        }
    }

    public void TeleportParticles()
    {
        Instantiate(particleBubblePop, transform.position, Quaternion.identity);
        Instantiate(particleBubbleTeleported, transform.position, Quaternion.identity);
    }
}