using UnityEngine;

public class BuildBox : MonoBehaviour
{
    [SerializeField] private int buildingStatus = 1;

    public int BuildingStatus
    {
        get { return buildingStatus; }
        set { buildingStatus = value; }
    }
}