using System;
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
        
        GameObject p1;
        GameObject p2;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ChangeState();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState();
            }
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
            if(p1) Destroy(p1);
            if(p2) Destroy(p2);
            
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
            var playerBuilderController1 = Instantiate(player1BuilderController);
            var playerBuilderController2 = Instantiate(player2BuilderController);
            p1 = playerBuilderController1.gameObject;
            p2 = playerBuilderController2.gameObject;
            cameraController.player1 = p1;
            cameraController.player2 = p2;
            placementManager.SetPlayers(playerBuilderController1, playerBuilderController2); 
        }
        
        private void StartPlaying()
        {

            p1 = Instantiate(player1Controller, playerStartPositions[0]).gameObject;
            p2 = Instantiate(player2Controller, playerStartPositions[1]).gameObject;
            cameraController.player1 = p1;
            cameraController.player2 = p2;
            gameState = GameState.Playing;
        }
    }

}
