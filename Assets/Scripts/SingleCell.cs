using UnityEngine;
using System.Collections;

public class SingleCell : MonoBehaviour
{
    [SerializeField] private MeshFilter getMeshFilter;
    [SerializeField] private Mesh[] meshes;

    private int collisions = 0;

    void OnTriggerEnter(Collider other)
    {
        collisions++;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BuildBox")
        {
            ChangeMesh(other.GetComponent<BuildingBuildBox>().GetCanBePlaced());
        }
    }

    void OnTriggerExit(Collider other)
    {
        collisions--;

        if (collisions == 0)
        {
            ChangeMesh(0);
        }
    }

    public void ChangeMesh(int setMesh = 0)
    {
        getMeshFilter.mesh = meshes[setMesh];
    }
}
