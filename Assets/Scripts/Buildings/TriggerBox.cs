using UnityEngine;
using System.Collections;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private BuildBox buildBox;
    [SerializeField] private bool isStatic = false;
    private bool isPlaced = false;
    private int collisions = 0;

    public bool IsPlaced
    {
        get { return isPlaced; }
        set { isPlaced = value; }
    }

    public void Detonate()
    {
        isPlaced = false;
        buildBox.BuildingStatus = 0;
        transform.position = new Vector3(transform.position.x, -40, transform.position.z);

        StartCoroutine("GetDestroyed");
    }

    void OnTriggerEnter(Collider other)
    {
        if (isStatic == true)
        {
            return;
        }

        collisions++;

        if (isPlaced == false)
        {
            buildBox.BuildingStatus = 2;
        }
        else
        {
            if (other.tag == "BombBox")
            {
                buildBox.BuildingStatus = 3;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isStatic == true)
        {
            return;
        }

        collisions--;

        if (isPlaced == false)
        {
            if (collisions == 0)
            {
                buildBox.BuildingStatus = 1;
            }
        }
        else
        {
            buildBox.BuildingStatus = 2;
        }
    }

    IEnumerator GetDestroyed()
    {
        yield return null;

        Destroy(transform.parent.gameObject);
    }
}