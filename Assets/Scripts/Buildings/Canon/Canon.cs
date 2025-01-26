using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float spawnInterval = 1f;

    private void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
