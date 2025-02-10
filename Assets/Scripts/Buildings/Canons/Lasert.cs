using UnityEngine;

public class Lasert : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3 portalStartPoint;
    [SerializeField] private Quaternion portalRotation;
    private bool portalHitted = false;

    public Vector3 PortalStartPoint
    {
        get { return portalStartPoint; }
    }

    public Quaternion PortalRotation
    {
        get { return portalRotation; }
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            if (hit.collider)
            {
                SetLasertsEndPoint(hit.point);
            }

            if (hit.collider.tag == "PortalBox" && portalHitted == false)
            {
                Portal getCollisionPortal = hit.collider.GetComponent<Portal>();
                GameObject getPortal = getCollisionPortal.GetPortal();

                if (getPortal != null)
                {
                    portalStartPoint = getPortal.transform.position;
                    portalRotation = getPortal.transform.rotation;

                    Actions.lasertPortalTouched?.Invoke(this);

                    portalHitted = true;
                }
            }
        }
        else
        {
            SetLasertsEndPoint(transform.up * 5000);
        }
    }

    public void SetLasertsStartPoint(Vector3 startPoint, Quaternion rotation)
    {
        transform.rotation = rotation;

        lineRenderer.SetPosition(0, startPoint);
    }

    public void SetLasertsEndPoint(Vector3 endPoint)
    {
        lineRenderer.SetPosition(1, endPoint);
    }
}
