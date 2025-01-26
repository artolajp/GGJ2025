using System.Collections.Generic;
using UnityEngine;
namespace GGJ2025
{
    public interface IGridable
    {
        public List<Vector2Int> GetPositions();
    }
}
