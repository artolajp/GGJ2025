using UnityEngine;
using System.Collections;

public class BulletPlane : Bullet
{
    private GameObject[] targets;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float lifespan = 5f;

    private GameObject selectedTarget;
    private Vector3 targetPosition;
    private bool choosePosition = true;

    protected override void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("PlayerBubble");
        StartCoroutine("DestroyAfterTime");

        if (targets.Length > 0)
        {
            selectedTarget = targets[Random.Range(0, targets.Length)];
        }
    }

    private void FixedUpdate()
    {
        if (selectedTarget != null)
        {
            targetPosition = selectedTarget.transform.position;
        }
        else
        {
            if (choosePosition == true)
            {
                targetPosition = GetRandomFarPoint();
                choosePosition = false;
            }
        }

        Vector3 direction = targetPosition - transform.position;

        direction.Normalize();

        Vector3 rotationAmount = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

        rigidBody.angularVelocity = -rotationAmount * rotateSpeed;
        rigidBody.linearVelocity = transform.forward * speed;
    }

    private Vector3 GetRandomFarPoint()
    {
        Vector3[] farPoints = new Vector3[]
        {
            new Vector3(40, 0, 40),
            new Vector3(-40, 0, -40),
            new Vector3(0, 0, 40),
            new Vector3(40, 0, 0)
        };

        return farPoints[Random.Range(0, farPoints.Length)];
    }

    protected override void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "WindBox")
        {
            WindBox fieldBox = collider.GetComponent<WindBox>();

            if (fieldBox != null)
            {
                speed += 4;

                float windStrength = fieldBox.WindForce * 4;
                Vector3 collisionNormal = collider.transform.forward;

                rigidBody.AddForce(collisionNormal * windStrength);
            }
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifespan);

        Instantiate(particleBullet, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
