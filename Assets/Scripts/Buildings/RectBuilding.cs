using GGJ2025;
using UnityEngine;

public class RectBuilding : Building
{
    [SerializeField] private GridObject grideable = new GridObject();
    protected override IGridable Grideable => grideable;
}
