using UnityEngine;

public class BuildingBuildBox : MonoBehaviour
{
    [SerializeField] private int canBePlaced = 1;
    private bool dontChange = false;

    public void SetCanBePlaced(int setCanBePlaced)
    {
        if (dontChange == false)
        {
            canBePlaced = setCanBePlaced;
        }
    }

    public int GetCanBePlaced()
    {
        return canBePlaced;
    }

    public void SetDontChange(bool setDontChange)
    {
        dontChange = setDontChange;
    }
}