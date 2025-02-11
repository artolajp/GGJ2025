using UnityEngine;

public class Lasert : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private CanonLasert myCanonLasert;
    [SerializeField] private Vector3 portalStartPoint;
    [SerializeField] private Quaternion portalRotation;
    private bool portalHitted = false;

    public CanonLasert MyCanonLasert
    {
        get { return myCanonLasert; }
        set { myCanonLasert = value; }
    }

    public Vector3 PortalStartPoint
    {
        get { return portalStartPoint; }
    }

    public Quaternion PortalRotation
    {
        get { return portalRotation; }
    }

    private void Awake()
    {
        CalculateLasert(false);
    }

    private void Update()
    {
        CalculateLasert(true);
    }

    private void CalculateLasert(bool checkPortals)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.right, out hit))
        {
            if (hit.collider)
            {
                SetLasertsEndPoint(hit.point);
            }

            if (checkPortals == true)
            {
                if (hit.collider.tag == "PortalBox")
                {
                    if (portalHitted == false)
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
                    if (portalHitted == true)
                    {
                        Actions.lasertPortalUntouched?.Invoke(this);

                        portalHitted = false;
                    }
                }
            }
        }
        else
        {
            SetLasertsEndPoint(transform.right * 5000);
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
