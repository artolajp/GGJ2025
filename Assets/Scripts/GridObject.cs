using UnityEngine;
namespace GGJ2025
{
    public class GridObject : IGridable
    {
        private int height;
        private int width;

        public GridObject() : this(1, 1)
        {
            
        }
        
        public GridObject(int width, int height)
        {
            this.height = height;
            this.width = width;
        }

        public Vector2Int Size()
        {
            return new Vector2Int(width, height);
        }
    }
}
