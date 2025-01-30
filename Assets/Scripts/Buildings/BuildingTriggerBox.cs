using UnityEngine;

public class BuildingTriggerBox : MonoBehaviour
{
    [SerializeField] private BuildingBuildBox buildBox;
    private int collisions = 0;

    void OnTriggerEnter(Collider other)
    {
        collisions++;
        buildBox.SetCanBePlaced(2);
    }

    void OnTriggerExit(Collider other)
    {
        collisions--;

        if (collisions == 0)
        {
            buildBox.SetCanBePlaced(1);
        }
    }
}