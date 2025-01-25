using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
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
            for (int i = 0; i < value.Size().x; i++)
            {
                for (int j = 0; j < value.Size().y; j++)
                {
                    if (x + i >= grid.GetLength(0) || y + j >= grid.GetLength(1)) { return false;}
                    if (x + i < 0 || y + j < 0) { return false;}
                    
                    if (grid[x + i, y + j] != null)
                    {
                        return false;
                    }
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
            
            if (x + value.Size().x > grid.GetLength(0) || y + value.Size().y > grid.GetLength(1))
            {
                return false;
            }
            
            if (x > grid.GetLength(0) || y > grid.GetLength(1))
            {
                return false;
            }

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
