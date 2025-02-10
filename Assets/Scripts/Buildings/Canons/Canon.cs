using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour
{
    [SerializeField] protected GameObject ammoSpawner;

    protected bool canShoot;
    protected Animator animator;
    [SerializeField] protected float animationSpeed = 1f;

    protected virtual void OnEnable()
    {
        Actions.canShoot += checkIfItCanShoot;

        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetFloat("SpeedMultiplier", 0f);
        }
    }

    protected virtual void OnDisable()
    {
        Actions.canShoot -= checkIfItCanShoot;
    }

    protected virtual void checkIfItCanShoot(bool setCanShoot)
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
