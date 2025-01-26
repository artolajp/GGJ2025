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
        
        [SerializeField] private PlayerBuilderController player1Builder;
        [SerializeField] private PlayerBuilderController player2Builder;
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

        public void SetPlayers(PlayerBuilderController player1Builder, PlayerBuilderController player2Builder)
        {
            this.player1Builder = player1Builder;
            this.player2Builder = player2Builder;

            player1Builder.OnConfirmed = OnConfirmedPlayer;
            player2Builder.OnConfirmed = OnConfirmedPlayer;
            
            player1Builder.BuildingInstance = Instantiate(player1Builder.building, player1Builder.transform.position, Quaternion.identity, player1Builder.transform);
            player2Builder.BuildingInstance = Instantiate(player2Builder.building, player2Builder.transform.position, Quaternion.identity, player2Builder.transform);
        }

        private void OnConfirmedPlayer(PlayerBuilderController player)
        {
            var x = (int)player.gameObject.transform.position.x - displacement.x;
            var y = (int)player.gameObject.transform.position.z - displacement.y;
            if (gridController.TryAdd(x, y, player.building.Grideable))
            {
                if (player1Builder == player)
                {
                    player1Builder.BuildingInstance.transform.SetParent(this.transform);
                    player1Builder.OnConfirmed = null;
                    player1Builder = null;
                }
                if (player2Builder == player)
                {
                    player2Builder.BuildingInstance.transform.SetParent(this.transform);
                    player2Builder.OnConfirmed = null;
                    player2Builder = null;
                }
                RefreshGrid();
            }
        }
        
        private void DrawPlayerBuilding(PlayerBuilderController playerBuilder)
        {
            if (playerBuilder)
            {
                var playerPosition = playerBuilder.transform.position;
                var targetCell = new Vector2Int((int)playerPosition.x, (int)playerPosition.z) - displacement;
                var targetPrefab = p1CellPrefab;
                if (!Controller.IsEmpty(targetCell.x, targetCell.y, playerBuilder.building.Grideable))
                {
                    targetPrefab = cellPrefab;
                }

                foreach (var position in playerBuilder.building.Grideable.GetPositions())
                {
                    tempCells.Add(Instantiate(targetPrefab, new Vector3(targetCell.x + displacement.x + position.x, 0.15f, targetCell.y + displacement.y + position.y), Quaternion.identity , transform));
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