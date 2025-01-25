using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class Player : MonoBehaviour
    {
        private Rigidbody rigidbody;
        private float movement_x;
        private float movement_y;

        [SerializeField]
        private float speed = 5f;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnMovement(InputValue movement_value)
        {
            Vector2 movement_vector = movement_value.Get<Vector2>();

            movement_x = movement_vector.x;
            movement_y = movement_vector.y;
        }

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(movement_x, 0.0f, movement_y);

            rigidbody.AddForce(movement * speed);
        }
    }

}