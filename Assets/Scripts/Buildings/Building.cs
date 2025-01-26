using System;
using System.Collections.Generic;
using GGJ2025;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private bool isBuilding = true;
    
    protected virtual IGridable Grideable { get; }

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
        
        gridController.TryAdd((int)transform.position.x,(int) transform.position.z, Grideable);
        
        onPlaced?.Invoke();
    }

    public bool CanPlace()
    {
        return isBuilding && gridController.IsEmpty((int)transform.position.x,(int) transform.position.z, Grideable);
    }
}
