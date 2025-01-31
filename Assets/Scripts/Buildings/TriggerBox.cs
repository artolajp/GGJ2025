using UnityEngine;

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
        Destroy(transform.parent.gameObject);
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
}