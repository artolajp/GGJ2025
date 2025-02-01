using UnityEngine;
using System.Collections;

public class BombBox : MonoBehaviour
{
    [SerializeField] private BuildBox buildBox;
    private TriggerBox triggerBox;
    private int collisions = 0;

    public void Detonate()
    {
        //Sound.
        //Particles.

        if (triggerBox != null && triggerBox.IsPlaced == true)
        {
            triggerBox.Detonate();
        }

        buildBox.BuildingStatus = 0;
        transform.position = new Vector3(transform.position.x, -40, transform.position.z);

        StartCoroutine("GetDestroyed");
    }

    void OnTriggerEnter(Collider other)
    {
        collisions++;

        if (other.tag == "TriggerBox")
        {
            triggerBox = other.GetComponent<TriggerBox>();
        }

        if (triggerBox != null)
        {
            if (triggerBox.IsPlaced == false)
            {
                buildBox.BuildingStatus = 2;
            }
            else
            {
                buildBox.BuildingStatus = 3;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        collisions--;

        if (collisions == 0)
        {
            triggerBox = null;
            buildBox.BuildingStatus = 3;
        }
    }

    IEnumerator GetDestroyed()
    {
        yield return null;

        Destroy(transform.parent.gameObject);
    }
}