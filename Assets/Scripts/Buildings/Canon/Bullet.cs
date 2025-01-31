using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private Rigidbody rigidBody;

    [SerializeField]
    private GameObject particleBullet;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_Bullet");
        Instantiate(particleBullet, transform.position, Quaternion.identity);
    }

    private void Start()
    {
        Vector3 direction = transform.forward;

        rigidBody.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(particleBullet, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(particleBullet, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
