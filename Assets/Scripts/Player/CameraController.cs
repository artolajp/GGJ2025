using UnityEngine;
using UnityEngine.Serialization;

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
    public GameObject player1;
    [SerializeField]
    public GameObject player2;
    
    private GameObject remainingPlayer = null;

    private bool playMusic = true;

    void Update()
    {
        if (playMusic == true)
        {
            FindObjectOfType<AudioController>().AudioPlaySoundVariation(1, 1, "Music_Level_01");
            playMusic = false;
        }

        Vector3 targetPosition = transform.position;

        if (player1 != null && player2 != null)
        {
            // Move the camera in the middle position of the two player positions.
            float distance = Vector3.Distance(player1.transform.position, player2.transform.position);

            targetPosition = (player1.transform.position + player2.transform.position) / 2;
            targetPosition.y = Mathf.Lerp(minimumDistance, maximumDistance, Mathf.InverseLerp(0, maximumDistance, distance) * zoomFactor);
            targetPosition.z -= 3f;
        }
        else if (!(player1 == null && player2 == null))
        {
            // Move the camera in the remaining player position.
            if (remainingPlayer == null)
            {
                targetPosition.z += 3f;

                if (player1 != null && player2 == null)
                {
                    remainingPlayer = player1;
                }
                else
                {
                    remainingPlayer = player2;
                }
            }

            targetPosition = remainingPlayer.transform.position;
            targetPosition.y = minimumDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
