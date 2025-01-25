using System.Collections.Generic;
using UnityEngine;
namespace GGJ2025
{
    public class PlacementManager : MonoBehaviour
    {
        private GridController<GridObject> gridController;
        private List<GameObject> cells = new List<GameObject>();
        private List<GameObject> tempCells = new List<GameObject>();
        
        [SerializeField] private PlayerController playerController;
        

        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject emptyCellPrefab;
        [SerializeField] private GameObject p1CellPrefab;
        
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

            var playerPosition = playerController.transform.position;
            targetCell = new Vector2Int((int)playerPosition.x, (int)playerPosition.z);
            
            foreach (var cell in tempCells)
            {
                Destroy(cell.gameObject);
            }
            tempCells.Clear();

            var targetPrefab = p1CellPrefab;
            if (!gridController.IsEmpty(targetCell.x, targetCell.y, new GridObject(targetSize.x, targetSize.y)))
            {
                targetPrefab = cellPrefab;
            }
            
            for (int x = targetCell.x; x < targetCell.x + targetSize.x; x++)
            {
                for (int y = targetCell.y; y < targetCell.y + targetSize.y; y++)
                {
                    tempCells.Add(Instantiate(targetPrefab, new Vector3(x, 0.15f, y), Quaternion.identity , transform));
                }
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
                cells.Add(Instantiate(emptyCellPrefab, new Vector3(empty.x, 0.1f, empty.y), Quaternion.identity, transform));
            }
            
            foreach (var cell in occupied)
            {
                cells.Add(Instantiate(cellPrefab, new Vector3(cell.x, 0.1f, cell.y), Quaternion.identity , transform));
            }
            
            
        }
    }

}