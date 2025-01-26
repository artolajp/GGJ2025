using System.Collections.Generic;
using GGJ2025;
using UnityEngine;

public class CustomBuilding : Building
{
    [SerializeField] private CustomGridObject grideable = new CustomGridObject(new List<Vector2Int>());
    protected override IGridable Grideable => grideable;
}
