using System.Collections.Generic;
using UnityEngine;
namespace GGJ2025
{
    public class PlacementManager : MonoBehaviour
    {
        private GridController<IGridable> gridController;
        private List<GameObject> cells = new List<GameObject>();
        private List<GameObject> tempCells = new List<GameObject>();
        
        [SerializeField] private PlayerController playerController;
        

        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject emptyCellPrefab;
        [SerializeField] private GameObject p1CellPrefab;
        
        [SerializeField] Vector2Int targetCell;
        [SerializeField] Vector2Int targetSize = Vector2Int.one;
        [SerializeField] List<Vector2Int> cellShape = new List<Vector2Int>(){Vector2Int.zero};
        public GridController<IGridable> Controller => gridController;
        
        private bool isActive = true;

        void Start()
        {
            gridController = new GridController<IGridable>(16, 20);
            RefreshGrid();
        }
        
        void Update()
        {
            //var gridObject = new GridObject(targetSize.x, targetSize.y);
            var gridObject = new CustomGridObject(cellShape);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TryAdd(targetCell.x, targetCell.y, gridObject);
                RefreshGrid();
            }
            
            foreach (var cell in tempCells)
            {
                Destroy(cell.gameObject);
            }
            tempCells.Clear();

            if(!isActive)return;
            
            var playerPosition = playerController.transform.position;
            targetCell = new Vector2Int((int)playerPosition.x, (int)playerPosition.z);
            
            var targetPrefab = p1CellPrefab;
            if (!Controller.IsEmpty(targetCell.x, targetCell.y, gridObject))
            {
                targetPrefab = cellPrefab;
            }

            foreach (var position in gridObject.GetPositions())
            {
                tempCells.Add(Instantiate(targetPrefab, new Vector3(targetCell.x + position.x, 0.15f, targetCell.y + position.y), Quaternion.identity , transform));
            }
        }
        
        public void RefreshGrid()
        {
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
            cells.Clear();
            
            if(!isActive)return;
                
            var empties = Controller.GetEmpties();
            var occupied = Controller.GetOccupied();

            foreach (var empty in empties)
            {
                cells.Add(Instantiate(emptyCellPrefab, new Vector3(empty.x, 0.1f, empty.y), Quaternion.identity, transform));
            }
            
            foreach (var cell in occupied)
            {
                cells.Add(Instantiate(cellPrefab, new Vector3(cell.x, 0.1f, cell.y), Quaternion.identity , transform));
            }
        }
        
        public void PauseGrid()
        {
            isActive = false;
            RefreshGrid();
        }
    }

}