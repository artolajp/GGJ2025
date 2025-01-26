using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Bullet Settings")]
    [SerializeField]
    private float speed = 10f;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Vector3 direction = transform.forward;

        rigidbody.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
