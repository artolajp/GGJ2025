using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
    private List<GameObject> portals = new List<GameObject>();
    private bool teleportDelay = false;

    [SerializeField] private GameObject portal;

    [SerializeField] private float getPortalSpeed;
    private float portalFrameSpeed = 2f;

    private void OnEnable()
    {
        portalFrameSpeed = getPortalSpeed;
        Actions.PortalBuilded += PortalPlaced;
    }

    private void OnDisable()
    {
        Actions.PortalBuilded -= PortalPlaced;
    }

    private void Update()
    {
        portal.transform.Rotate(0, portalFrameSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleportDelay == true)
        {
            return;
        }

        if (portals.Count != 0)
        {
            if (other.tag == "PlayerBubble")
            {
                FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.2f, 1.2f, "Sound_BubbleTeleport_1", "Sound_BubbleTeleport_2");

                GameObject getPortal = portals[(UnityEngine.Random.Range(0, portals.Count))];

                getPortal.GetComponent<Portal>().StartTeleportDelay();
                other.GetComponent<PlayerController>().TeleportParticles();
                other.transform.position = getPortal.transform.position;
                other.GetComponent<PlayerController>().TeleportParticles();

                StartTeleportDelay();
            }
        }
    }

    public void StartTeleportDelay()
    {
        StartCoroutine("TeleportDelay");
    }

    private void PortalPlaced()
    {
        portals.Clear();

        GameObject[] getPortals = GameObject.FindGameObjectsWithTag("PortalBox");

        foreach (GameObject portal in getPortals)
        {
            if (portal.transform != transform)
            {
                portals.Add(portal);
            }
        }
    }

    private IEnumerator TeleportDelay()
    {
        teleportDelay = true;
        portalFrameSpeed = 0.2f;

        yield return new WaitForSeconds(1f);

        teleportDelay = false;
        portalFrameSpeed = getPortalSpeed;
    }
}
