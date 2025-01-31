using UnityEngine;

public class SingleCell : MonoBehaviour
{
    [SerializeField] private MeshFilter getMeshFilter;
    [SerializeField] private Mesh[] meshes;
    private int mesh = 0;

    private int collisions = 0;

    private void Update()
    {
        if (collisions == 0)
        {
            if (mesh != 0)
            {
                ChangeMesh(0);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        collisions++;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BuildBox")
        {
            ChangeMesh(other.GetComponent<BuildBox>().BuildingStatus);
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
        mesh = setMesh;
        getMeshFilter.mesh = meshes[mesh];
    }
}
