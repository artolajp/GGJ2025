using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GGJ2025;

public class PlayerController : MonoBehaviour
{
    // Properties.
    [Header("Player properties.")]
    [SerializeField] private int playerNumber = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject particleBubblePop;

    // Movement.
    private Rigidbody rigidbody;
    private float movement_x;
    private float movement_y;
    public Action<PlayerController> OnDead;
    public Action<PlayerController> OnScore;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMovement(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movement_x = movementVector.x;
        movement_y = movementVector.y;
    }

    private void OnRotate()
    {
        Debug.Log("Pressed!");
    }
        
    private void OnConfirm()
    {
        Debug.Log("Confirmed!");
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movement_x, 0.0f, movement_y);

        rigidbody.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_BubblePop_01", "Sound_BubblePop_02", "Sound_BubblePop_03");

        if (other.tag == "HurtBox")
        {
            Instantiate(particleBubblePop, transform.position, Quaternion.identity);

            OnDead?.Invoke(this);
            Destroy(gameObject);
        }
            
        if (other.tag == "WinBox")
        {
            FindObjectOfType<AudioController>().AudioPlaySoundVariation(1, 1, "Sound_Win");
            
            Instantiate(particleBubblePop, transform.position, Quaternion.identity);
            
            OnScore?.Invoke(this);
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

                rigidbody.AddForce(collisionNormal * windStrength);
            }
        }
    }
}