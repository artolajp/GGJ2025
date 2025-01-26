using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class PlayerController : MonoBehaviour
    {
        // Properties.
        [Header("Player properties.")]
        [SerializeField]
        private int playerNumber = 0;

        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private GameObject particleBubblePop;

        // Movement.
        private Rigidbody rigidbody;
        private float movement_x;
        private float movement_y;
        public Action<PlayerController> OnDead;
        private bool isDead = false;

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
            if (other.tag == "HurtBox")
            {
                // Play sound.
                FindObjectOfType<AudioController>().AudioPlaySoundVariation(1, 1.2f, "Sound_ExplodingBalloon_1", "Sound_ExplodingBalloon_2", "Sound_ExplodingBalloon_3");

                // Send event to controller.

                Instantiate(particleBubblePop, transform.position, Quaternion.identity);
                OnDead?.Invoke(this);
                
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
}