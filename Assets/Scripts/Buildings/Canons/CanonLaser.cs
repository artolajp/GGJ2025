using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanonLaser : Canon
{
    [SerializeField] private Lasert lasert;
    private List<Lasert> laserts;

    protected override void OnEnable()
    {
        base.OnEnable();

        Actions.lasertPortalTouched += lasertPortal;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Actions.lasertPortalTouched -= lasertPortal;
    }

    public void SpawnLaserts(Vector3 startPoint, Quaternion rotation)
    {
        if (canShoot == true)
        {
            Lasert newLasert = Instantiate(lasert, startPoint, rotation).GetComponent<Lasert>();
            newLasert.SetLasertsStartPoint(startPoint, rotation);

            laserts.Add(newLasert);
        }
    }

    private void lasertAmmoSpawner()
    {
        SpawnLaserts(ammoSpawner.transform.position, ammoSpawner.transform.rotation);
    }

    private void lasertPortal(Lasert getLasert)
    {
        SpawnLaserts(getLasert.PortalStartPoint, getLasert.PortalRotation);
    }

    private void DestroyLaserts()
    {
        if (laserts.Count != 0)
        {
            foreach (Lasert lasert in laserts)
            {
                Destroy(lasert.gameObject);
            }
        }

        laserts.Clear();
    }
}
