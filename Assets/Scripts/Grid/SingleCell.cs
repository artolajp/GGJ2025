using UnityEngine;

public class SingleCell : MonoBehaviour
{
    [SerializeField] private MeshFilter getMeshFilter;
    [SerializeField] private Mesh[] Meshes;

    private int gridState = 0;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void ChangeMesh(int currentMesh = 0)
    {
        getMeshFilter.mesh = Meshes[currentMesh];
    }

    void OnTriggerEnter(Collider other)
    {
        ChangeMesh(2);
    }
}
