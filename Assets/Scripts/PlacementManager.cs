using System.Collections.Generic;
using UnityEngine;
namespace GGJ2025
{
    public class PlacementManager : MonoBehaviour
    {
        private GridController<GridObject> gridController;
        private List<GameObject> cells = new List<GameObject>();
        

        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject emptyCellPrefab;
        
        [SerializeField] Vector2Int targetCell;
        [SerializeField] Vector2Int targetSize = Vector2Int.one;
        
        void Start()
        {
            gridController = new GridController<GridObject>(16, 16);
            RefreshGrid();
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gridController.TryAdd(targetCell.x, targetCell.y, new GridObject(targetSize.x, targetSize.y));
                RefreshGrid();
            }
        }
        
        private void RefreshGrid()
        {

            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
            cells.Clear();
            
            var empties = gridController.GetEmpties();
            var occupied = gridController.GetOccupied();

            foreach (var empty in empties)
            {
                cells.Add(Instantiate(emptyCellPrefab, new Vector3(empty.x, 0, empty.y), Quaternion.identity, transform));
            }
            
            foreach (var cell in occupied)
            {
                cells.Add(Instantiate(cellPrefab, new Vector3(cell.x, 0, cell.y), Quaternion.identity , transform));
            }
        }
    }

}