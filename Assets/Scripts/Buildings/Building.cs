using System;
using System.Collections.Generic;
using GGJ2025;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private bool isBuilding = true;
    [SerializeField] private Vector2Int position = new Vector2Int(0, 0);
    [SerializeField] private bool isInitialBuilding = true;
    
    public virtual IGridable Grideable { get; }

    public Action onPlaced;
    public int owner;
    public GridController<IGridable> gridController;

    public void Initialize(GridController<IGridable> gridController, int owner)
    {
        this.gridController = gridController;
        this.owner = owner;
    }

    public void ConfirmPlacement()
    {
        if(!CanPlace()) return;
        
        isBuilding = false;
        if (isInitialBuilding)
        {
            gridController.TryAdd(position.x, position.y, Grideable);
            onPlaced?.Invoke();
        }
        else
        {
            gridController.TryAdd((int)transform.position.x,(int) transform.position.z, Grideable);
            onPlaced?.Invoke();
        }
    }

    public bool CanPlace()
    {
        if (isInitialBuilding)
        {
            return isBuilding && gridController.IsEmpty(position.x, position.y, Grideable);
        }
        return isBuilding && gridController.IsEmpty((int)transform.position.x,(int) transform.position.z, Grideable);
    }
}
