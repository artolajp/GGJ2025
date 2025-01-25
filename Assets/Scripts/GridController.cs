using System;
namespace GGJ2025
{
    public class GridController<T> where T : class
    {
        private T[,] grid;
        
        public T this[int x, int y] => grid[y,x]; 

        public GridController(int width, int height)
        {
            grid = new T[width,height];
        }


        public void Add(int x, int y, T value)
        {
            grid[y,x] = value;
        }
        public void Remove(int x, int y)
        {
            grid[y, x] = null;
        }
    }
}
