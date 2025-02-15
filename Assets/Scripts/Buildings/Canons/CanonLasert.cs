using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanonLasert : Canon
{
    [SerializeField] private Lasert lasert;
    private List<Lasert> laserts = new List<Lasert>();

    protected override void OnEnable()
    {
        base.OnEnable();

        Actions.lasertPortalTouched += lasertPortal;
        Actions.lasertPortalUntouched += lasertPortalUntouched;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Actions.lasertPortalTouched -= lasertPortal;
        Actions.lasertPortalUntouched -= lasertPortalUntouched;
    }

    public void SpawnLaserts(Vector3 startPoint, Quaternion rotation)
    {
        if (laserts.Count < 5)
        {
            Lasert getLasert = Instantiate(lasert, startPoint, rotation);
            getLasert.GetComponent<Lasert>();

            getLasert.MyCanonLasert = this;
            getLasert.SetLasertsStartPoint(startPoint, rotation);

            laserts.Add(getLasert);
        }
    }

    private void lasertAmmoSpawner()
    {
        SpawnLaserts(ammoSpawner.transform.position, ammoSpawner.transform.rotation);
    }

    private void lasertPortal(Lasert getLasert)
    {
        if (getLasert.MyCanonLasert == this)
        {
            SpawnLaserts(getLasert.PortalStartPoint, getLasert.PortalRotation);
        }
    }

    private void lasertPortalUntouched(Lasert getLasert)
    {
        if (laserts.Count != 0)
        {
            for (int iterator = laserts.IndexOf(getLasert) + 1; iterator < laserts.Count; iterator++)
            {
                laserts[iterator].DestroyPartices();
                Destroy(laserts[iterator].gameObject);
                laserts.RemoveAt(iterator);

                iterator--;
            }
        }
    }

    private void DestroyLaserts()
    {
        if (laserts.Count != 0)
        {
            foreach (Lasert lasert in laserts)
            {
                lasert.DestroyPartices();
                Destroy(lasert.gameObject);
            }
        }

        laserts.Clear();
    }

    protected override void checkIfItCanShoot(bool setCanShoot)
    {
        canShoot = setCanShoot;

        if (animator != null)
        {
            if (canShoot == false)
            {
                DestroyLaserts();
                animator.SetFloat("SpeedMultiplier", 0f);
            }
            else
            {
                animator.SetFloat("SpeedMultiplier", animationSpeed);
            }
        }
    }
}
