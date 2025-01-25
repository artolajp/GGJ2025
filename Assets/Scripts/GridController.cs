using System.Collections.Generic;
using UnityEngine;
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
        public bool IsEmpty (int x, int y, T value)
        {
            foreach (var pos in value.GetPositions())
            {
                if (x + pos.x >= grid.GetLength(0) || y + pos.y >= grid.GetLength(1)) { return false;}
                if (x + pos.x < 0 || y + pos.y < 0) { return false;}
                
                if (grid[x + pos.x, y + pos.y] != null)
                {
                    return false;
                }
            }
            
            return true;
        }

        public bool TryAdd(int x, int y, T value)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }
            var xMax = 0;
            var yMax = 0;
            var positions = value.GetPositions();
            
            foreach (var position in positions)
            {
                if(position.x > xMax) xMax = position.x;
                if(position.y > yMax) yMax = position.y;
            }
            
            if (x + xMax >= grid.GetLength(0) || y + yMax >= grid.GetLength(1))
            {
                return false;
            }
            
            if (x >= grid.GetLength(0) || y >= grid.GetLength(1))
            {
                return false;
            }

            foreach (var position in positions)
            {
                if (grid[x + position.x, y + position.y] != null) { return false; }
            }
            
            foreach (var position in positions)
            {
                grid[x + position.x, y + position.y] = value;
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
        public List<Vector2Int> GetEmpties()
        {
            var empties = new List<Vector2Int>();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if(grid[x, y] == null) { empties.Add(new Vector2Int(x, y)); }
                }
            }

            return empties;
        }
        
        public List<Vector2Int> GetOccupied()
        {
            var empties = new List<Vector2Int>();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if(grid[x, y] != null) { empties.Add(new Vector2Int(x, y)); }
                }
            }

            return empties;
        }
    }
}
