using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025
{
    public class GameManager : MonoBehaviour
    {
        enum GameState {Init ,Playing, Building, Score, EndScreen, StartScreen}
        
        [SerializeField] GameState gameState = GameState.Init;
        [SerializeField] private List<Building> initialBuildings = new List<Building>();
        [SerializeField] private PlacementManager placementManager = null;
        [SerializeField] private CameraController cameraController = null;
        
        [SerializeField] private int currentScore = 0;
        [SerializeField] private int targetScore = 0;
        
        [SerializeField] private PlayerController player1Controller = null;
        [SerializeField] private PlayerController player2Controller = null;
        [SerializeField] private PlayerBuilderController player1BuilderController = null;
        [SerializeField] private PlayerBuilderController player2BuilderController = null;
        [SerializeField] private List<Transform> playerStartPositions = new();
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ChangeState();
        }

        void InitBuildings()
        {
            foreach (var building in initialBuildings)
            {
                building.Initialize(placementManager.Controller, 0);
                building.ConfirmPlacement();
            }

            placementManager.RefreshGrid();
        }

        private void ChangeState()
        {
            switch (gameState)
            {
                case GameState.Init:
                    InitBuildings();
                    StartPlaying();
                    break;
                case GameState.Playing when currentScore < targetScore:
                    StartBuilding();
                    break;
                case GameState.Playing:
                    gameState = GameState.EndScreen;
                    break;
                case GameState.Building:
                    StartPlaying();
                    break;
                default:
                    gameState = GameState.EndScreen;
                    break;
            }
        }
        private void StartBuilding()
        {
            gameState = GameState.Building;
            var p1BuilderController = Instantiate(player1BuilderController);
            var p2BuilderController = Instantiate(player2BuilderController);
            cameraController.player1 = p1BuilderController.gameObject;
            cameraController.player2 = p2BuilderController.gameObject;
        }
        private void StartPlaying()
        {

            var p1Controller = Instantiate(player1Controller, playerStartPositions[0]);
            var p2Controller = Instantiate(player2Controller, playerStartPositions[1]);
            cameraController.player1 = p1Controller.gameObject;
            cameraController.player2 = p2Controller.gameObject;
            gameState = GameState.Playing;
        }
    }

}
