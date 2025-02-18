using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    protected Rigidbody rigidBody;

    [SerializeField] protected GameObject particleBullet;
    [SerializeField] protected string spawnSound = "Sound_CanonShoot_1";
    [SerializeField] protected string destroySound = "Sound_Bullet_1";

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(0.9f, 1.1f, spawnSound);
        Instantiate(particleBullet, transform.position, Quaternion.identity);
    }

    protected abstract void Start();

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PortalBox" || collider.tag == "Respawn" || collider.tag == "Finish" || collider.tag == "WindBox")
        {
            return;
        }

        FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(0.9f, 1.1f, destroySound);

        Instantiate(particleBullet, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "WindBox")
        {
            WindBox fieldBox = collider.GetComponent<WindBox>();

            if (fieldBox != null)
            {
                float windStrength = fieldBox.WindForce;
                Vector3 collisionNormal = collider.transform.forward;

                rigidBody.AddForce(collisionNormal * windStrength);
            }
        }
    }

    public virtual void TeleportParticles()
    {
        Instantiate(particleBullet, transform.position, Quaternion.identity);
    }
}
