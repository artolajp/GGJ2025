using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Bullet Settings")]
    [SerializeField]
    private float speed = 10f;

    private Rigidbody rigidbody;

    [SerializeField]
    private GameObject particleBullet;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        FindObjectOfType<AudioController>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_Bullet");
        Instantiate(particleBullet, transform.position, Quaternion.identity);
    }

    private void Start()
    {
        Vector3 direction = transform.forward;

        rigidbody.linearVelocity = direction * speed;
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
