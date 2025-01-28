using UnityEngine;

public class SingleCell : MonoBehaviour
{
    [SerializeField] private MeshFilter getMeshFilter;
    [SerializeField] private Mesh[] meshes;

    private int gridMesh = 0;
    private int collisions = 0;

    void OnTriggerEnter(Collider other)
    {
        collisions += 1;

        if (other.tag == "Building")
        {
            if (other.GetComponent<Building>().CanBePlaced())
            {
                ChangeMesh(1);
            }
            else
            {
                ChangeMesh(2);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        collisions -= 1;

        if (collisions == 0)
        {
            if (gridMesh != 0)
            {
                ChangeMesh(0);
            }
        }
    }

    public void ChangeMesh(int setMesh = 0)
    {
        gridMesh = setMesh;
        getMeshFilter.mesh = meshes[gridMesh];
    }
}
