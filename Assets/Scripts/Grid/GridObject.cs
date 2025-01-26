using System;
using System.Collections.Generic;
using UnityEngine;
namespace GGJ2025
{
    [Serializable]
    public class GridObject : IGridable
    {
        [SerializeField] private int height;
        [SerializeField] private int width;

        public GridObject() : this(1, 1)
        {
            
        }
        
        public GridObject(int width, int height)
        {
            this.height = height;
            this.width = width;
        }

        public List<Vector2Int> GetPositions()
        {
            var positions = new List<Vector2Int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }

            return positions;
        }
    }

}
