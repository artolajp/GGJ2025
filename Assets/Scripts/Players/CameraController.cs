using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float minimumDistance = 10f;
    [SerializeField] private float maximumDistance = 30f;
    [SerializeField] private float zoomFactor = 1f;

    public GameObject player_01;
    public GameObject player_02;
    private GameObject remainingPlayer = null;

    private bool playMusic = true;

    void Update()
    {
        if (playMusic == true)
        {
            FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(1, 1, "Music_Level_01");
            playMusic = false;
        }

        Vector3 targetPosition = transform.position;

        if (player_01 != null && player_02 != null)
        {
            // Move the camera in the middle position of the two player positions.
            float distance = Vector3.Distance(player_01.transform.position, player_02.transform.position);

            targetPosition = (player_01.transform.position + player_02.transform.position) / 2;
            targetPosition.y = Mathf.Lerp(minimumDistance, maximumDistance, Mathf.InverseLerp(0, maximumDistance, distance) * zoomFactor);
            targetPosition.z -= 3f;
        }
        else if (!(player_01 == null && player_02 == null))
        {
            // Move the camera in the remaining player position.
            if (remainingPlayer == null)
            {
                targetPosition.z += 3f;

                if (player_01 != null && player_02 == null)
                {
                    remainingPlayer = player_01;
                }
                else
                {
                    remainingPlayer = player_02;
                }
            }

            targetPosition = remainingPlayer.transform.position;
            targetPosition.y = minimumDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
