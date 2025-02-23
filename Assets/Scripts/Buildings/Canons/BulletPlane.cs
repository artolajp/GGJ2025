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
            Debug.Log(targets.Length);
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
        Vector3 targetForce = transform.forward * speed;

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
                Vector3 windDirection = collider.transform.forward;
                float windStrength = fieldBox.WindForce * 8;

                float dotProduct = Vector3.Dot(transform.forward, windDirection);

                float speedAdjustment = dotProduct;
                float newSpeed = speed + speedAdjustment;

                rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * newSpeed;

                rigidBody.AddForce(windDirection * windStrength);
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
