using UnityEngine;

public class SingleCell : MonoBehaviour
{
    [SerializeField] private MeshFilter getMeshFilter;
    [SerializeField] private Mesh[] meshes;

    private int collisions = 0;

    void OnTriggerEnter(Collider other)
    {
        collisions++;

        if (other.tag == "HitBox" || other.tag == "Respawn" || other.tag == "Finish")
        {
            ChangeMesh(2);
        }

        if (other.tag == "Building")
        {
            ChangeMesh(other.GetComponent<Building>().CanBePlaced());
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
