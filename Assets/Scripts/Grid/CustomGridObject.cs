using System;
using System.Collections.Generic;
using UnityEngine;
namespace GGJ2025
{
    [Serializable]
    public class CustomGridObject : IGridable
    {
        [SerializeField] private List<Vector2Int> positions = new ();
        
        public List<Vector2Int> GetPositions() => positions;

        public CustomGridObject(List<Vector2Int> positions)
        {
            this.positions = positions;
        }
    }
}
