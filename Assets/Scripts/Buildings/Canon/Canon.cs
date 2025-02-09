using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawner;

    private bool canShoot;
    private Animator animator;

    [SerializeField] private float animationSpeed = 1f;

    private void OnEnable()
    {
        Actions.canShoot += checkIfItCanShoot;

        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetFloat("SpeedMultiplier", 0f);
        }
    }

    private void OnDisable()
    {
        Actions.canShoot -= checkIfItCanShoot;
    }

    public void SpawnBullets()
    {
        if (canShoot == true)
        {
            Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        }
    }

    private void checkIfItCanShoot(bool setCanShoot)
    {
        canShoot = setCanShoot;

        if (animator != null)
        {
            if (canShoot == false)
            {
                animator.SetFloat("SpeedMultiplier", 0f);
            }
            else
            {
                animator.SetFloat("SpeedMultiplier", animationSpeed);
            }
        }
    }
}
