using UnityEngine;
using System.Collections.Generic;

public class SingleCell : CollisionComponent
{
    [SerializeField] private MeshFilter getMeshFilter;
    [SerializeField] private Mesh[] meshes;
    private int mesh = 0;

    private void FixedUpdate()
    {
        CleanUpColliders();

        if (colliders.Count != 0)
        {
            int iterator = 0;

            foreach (Collider collider in colliders)
            {
                if (collider.tag == "BuildBox")
                {
                    int buildingStatus = collider.GetComponent<BuildBox>().BuildingStatus;

                    if (iterator > 0)
                    {
                        if (buildingStatus > mesh)
                        {
                            ChangeMesh(buildingStatus);
                        }
                    }
                    else
                    {
                        ChangeMesh(buildingStatus);
                    }
                }

                iterator++;
            }

            colliders.Clear();
        }
        else
        {
            ChangeMesh(0);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        AddCollider(collider);
    }

    public void ChangeMesh(int setMesh = 0)
    {
        if (mesh != setMesh)
        {
            mesh = setMesh;
            getMeshFilter.mesh = meshes[mesh];
        }
    }
}
