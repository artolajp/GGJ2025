using UnityEngine;

public class Lasert : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject particleLasert;
    [SerializeField] private int lengthOfLineRenderer = 8;
    private ParticleSystem particleLasertStart;
    private ParticleSystem particleLasertEnd;
    private CanonLasert myCanonLasert;
    private Vector3 portalStartPoint;
    private Quaternion portalRotation;
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
            if (hit.collider && hit.collider.tag != "PlayerBubble")
            {
                SetLasertsEndPoint(hit.point);
            }

            if (hit.collider.tag == "PlayerBubble")
            {
                PlayerController getPlayer = hit.collider.GetComponent<PlayerController>();
                getPlayer.BubblePop(true);
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
        if (particleLasertStart == null)
        {
            GameObject getParticle = Instantiate(particleLasert, startPoint, rotation);
            particleLasertStart = getParticle.GetComponent<ParticleSystem>();
        }

        transform.rotation = rotation;

        lineRenderer.SetPosition(0, startPoint);
    }

    public void SetLasertsEndPoint(Vector3 endPoint)
    {
        if (particleLasertEnd == null)
        {
            GameObject getParticle = Instantiate(particleLasert, endPoint, transform.rotation);
            particleLasertEnd = getParticle.GetComponent<ParticleSystem>();
        }
        else
        {
            particleLasertEnd.gameObject.transform.position = endPoint;
        }

        var points = new Vector3[lengthOfLineRenderer];

        for (int iterator = 0; iterator < lengthOfLineRenderer; iterator++)
        {
            points[iterator] = Vector3.Lerp(transform.position, endPoint, (float)iterator / (lengthOfLineRenderer - 1));
        }

        lineRenderer.SetPositions(points);
    }

    public void DestroyPartices()
    {
        if (particleLasertStart != null)
        {
            particleLasertStart.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        if (particleLasertEnd != null)
        {
            particleLasertEnd.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}