using System.Collections.Generic;
using UnityEngine;
namespace GGJ2025
{
    public class PlacementManager : MonoBehaviour
    {
        private GridController<IGridable> gridController;
        private List<GameObject> cells = new List<GameObject>();
        private List<GameObject> tempCells = new List<GameObject>();
        

        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject emptyCellPrefab;
        [SerializeField] private GameObject p1CellPrefab;
        [SerializeField] private Vector2Int displacement;
        [SerializeField] private int width;
        [SerializeField] private int height;
        
        [SerializeField] PlayerBuilderController player1Builder;
        [SerializeField] PlayerBuilderController player2Builder;
        public GridController<IGridable> Controller => gridController;
        
        private bool isActive = true;

        void Start()
        {
            gridController = new GridController<IGridable>(width, height);
            RefreshGrid();
        }
        
        void Update()
        {
            foreach (var cell in tempCells)
            {
                Destroy(cell.gameObject);
            }
            tempCells.Clear();

            if(!isActive)return;

            DrawPlayerBuilding(player1Builder);
            DrawPlayerBuilding(player2Builder);
        }
        private void DrawPlayerBuilding(PlayerBuilderController playerBuilder)
        {
            if (playerBuilder != null)
            {
                var playerPosition = playerBuilder.transform.position;
                var targetCell = new Vector2Int((int)playerPosition.x, (int)playerPosition.z) + displacement;
            
                var targetPrefab = p1CellPrefab;
                if (!Controller.IsEmpty(targetCell.x, targetCell.y, playerBuilder.building.Grideable))
                {
                    targetPrefab = cellPrefab;
                }

                foreach (var position in playerBuilder.building.Grideable.GetPositions())
                {
                    tempCells.Add(Instantiate(targetPrefab, new Vector3(targetCell.x + position.x, 0.15f, targetCell.y + position.y), Quaternion.identity , transform));
                }
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
                cells.Add(Instantiate(emptyCellPrefab, new Vector3(displacement.x + empty.x, 0.1f, displacement.y + empty.y), Quaternion.identity, transform));
            }
            
            foreach (var cell in occupied)
            {
                cells.Add(Instantiate(cellPrefab, new Vector3(displacement.x + cell.x, 0.1f, displacement.y + cell.y), Quaternion.identity , transform));
            }
        }
        
        public void PauseGrid()
        {
            isActive = false;
            RefreshGrid();
        }
    }

}