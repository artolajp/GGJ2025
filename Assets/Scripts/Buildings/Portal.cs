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

    private void OnTriggerEnter(Collider collider)
    {
        if (teleportDelay == true)
        {
            return;
        }

        if (portals.Count != 0)
        {
            if (collider.tag == "PlayerBubble")
            {
                GameObject getPortal = GetPortal();

                if (getPortal != null)
                {
                    FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.2f, 1.2f, "Sound_BubbleTeleport_1", "Sound_BubbleTeleport_2");

                    getPortal.GetComponent<Portal>().StartTeleportDelay();
                    collider.GetComponent<PlayerController>().TeleportParticles();
                    collider.transform.position = getPortal.transform.position;
                    collider.GetComponent<PlayerController>().TeleportParticles();

                    StartTeleportDelay();
                }
            }

            if (collider.tag == "HurtBox")
            {
                if (collider.transform.parent.tag == "Bullet")
                {
                    GameObject getPortal = GetPortal();

                    if (getPortal != null)
                    {
                        FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.2f, 1.2f, "Sound_Teleported_1");

                        Bullet getBullet = collider.transform.parent.GetComponent<Bullet>();

                        getPortal.GetComponent<Portal>().StartTeleportDelay();
                        getBullet.GetComponent<Bullet>().TeleportParticles();
                        getBullet.transform.position = getPortal.transform.position;
                        getBullet.transform.rotation = getPortal.transform.rotation;
                        getBullet.GetComponent<Bullet>().TeleportParticles();

                        StartTeleportDelay();
                    }
                }
            }
        }
    }

    public GameObject GetPortal()
    {
        if (portals.Count != 0)
        {
            return portals[(UnityEngine.Random.Range(0, portals.Count))];
        }

        return null;
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
