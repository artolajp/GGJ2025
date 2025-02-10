using UnityEngine;
using System.Collections;

public class CanonBullet : Canon
{
    [SerializeField] private GameObject bullet;

    public void SpawnBullets()
    {
        if (canShoot == true)
        {
            Instantiate(bullet, ammoSpawner.transform.position, ammoSpawner.transform.rotation);
        }
    }
}
