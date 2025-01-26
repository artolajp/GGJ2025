using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Properties.
    [Header("Camera properties.")]
    [SerializeField]
    private float lerpSpeed = 5f;
    [SerializeField]
    private float minimumDistance = 10f;
    [SerializeField]
    private float maximumDistance = 30f;
    [SerializeField]
    private float zoomFactor = 1f;

    [SerializeField]
    private GameObject player_1;
    [SerializeField]
    private GameObject player_2;
    private GameObject remainingPlayer = null;

    void Update()
    {
        Vector3 targetPosition = transform.position;

        if (player_1 != null && player_2 != null)
        {
            // Move the camera in the middle position of the two player positions.
            float distance = Vector3.Distance(player_1.transform.position, player_2.transform.position);

            targetPosition = (player_1.transform.position + player_2.transform.position) / 2;
            targetPosition.y = Mathf.Lerp(minimumDistance, maximumDistance, Mathf.InverseLerp(0, maximumDistance, distance) * zoomFactor);
            targetPosition.z -= 3f;
        }
        else if (!(player_1 == null && player_2 == null))
        {
            // Move the camera in the remaining player position.
            if (remainingPlayer == null)
            {
                targetPosition.z += 3f;

                if (player_1 != null && player_2 == null)
                {
                    remainingPlayer = player_1;
                }
                else
                {
                    remainingPlayer = player_2;
                }
            }

            targetPosition = remainingPlayer.transform.position;
            targetPosition.y = minimumDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
