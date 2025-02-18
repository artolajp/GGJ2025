using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;

    void Start()
    {
        Actions.canShoot?.Invoke(true);
    }

    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}
