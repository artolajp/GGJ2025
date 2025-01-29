using UnityEngine;

public class Building : MonoBehaviour
{
    private int canBePlaced = 1;
    private int collisions = 0;

    void OnTriggerEnter(Collider other)
    {
        collisions++;

        if (other.tag == "Building" || other.tag == "HitBox")
        {
            canBePlaced = 2;
        }
    }

    void OnTriggerExit(Collider other)
    {
        collisions--;

        if (collisions == 0)
        {
            canBePlaced = 1;
        }
    }

    public int CanBePlaced()
    {
        return canBePlaced;
    }
}