using System;
using System.Data.SqlTypes;
using System.Drawing;
namespace GGJ2025
{
    public class GridController<T> where T : class, IGridable
    {
        private T[,] grid;
        
        public T this[int x, int y] => grid[x,y]; 

        public GridController(int width, int height)
        {
            grid = new T[width,height];
        }
        public bool IsEmpty (int x, int y) => grid[x,y] == null;

        public bool TryAdd(int x, int y, T value)
        {
            if (value.Size().x > grid.GetLength(0) || value.Size().y > grid.GetLength(1))
            {
                return false;
            }
            
            if(grid[y,x] != null) { return false; }

            for (int i = 0; i < value.Size().x; i++)
            {
                for (int j = 0; j < value.Size().y; j++)
                {
                    if (grid[x + i, y + j] != null) { return false; }
                }
            }
            for (int i = 0; i < value.Size().x; i++)
            {
                for (int j = 0; j < value.Size().y; j++)
                {
                    grid[x + i, y + j] = value;
                }
            }
            return true;
        }
        
        public bool TryRemove(T value)
        {
            bool removed = false;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == value)
                    {
                        grid[x, y] = null;
                        removed = true;
                    }
                }
            }
            
            return removed;
        }
    }
}
